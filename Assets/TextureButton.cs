using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureButton : MonoBehaviour
{

	public BrushSO[] Brushes;

	public void SetBrush(Texture2D tex)
	{
		foreach (var b in Brushes)
		{
			if (b.brushTexture == tex)
			{
				GetComponent<Image>().sprite = b.brushSprite;		
			}
		}
	}
	
	public delegate void OnTextureChangeDelegate(Texture2D tex);
	public TexturePickerPanel TexturePicker;

	public event OnTextureChangeDelegate OnTextureChange;
	
	// Use this for initialization
	void Start()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(ShowPicker);
	}

	private void ShowPicker()
	{
		TexturePicker.gameObject.SetActive(true);
		TexturePicker.OnTextureSelected += OnSelected;
	}

	void OnSelected(Texture2D tex)
	{
		SetBrush(tex);
		OnTextureChange?.Invoke(tex);
	}
}
