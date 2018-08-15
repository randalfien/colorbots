using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorChangerButton : MonoBehaviour
{
	public delegate void OnColorChangeDelegate(Color botColor);
	public ColorPickerPanel ColorPicker;

	public event OnColorChangeDelegate OnColorChange;
	
	// Use this for initialization
	void Start()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(ShowPicker);
	}

	private void ShowPicker()
	{
		ColorPicker.gameObject.SetActive(true);
		ColorPicker.OnColorSelected += OnSelected;
	}

	void OnSelected(Color clr)
	{
		GetComponent<Image>().color = clr;
		OnColorChange.Invoke(clr);
	}

	public void SetColor(Color botColor)
	{
		GetComponent<Image>().color = botColor;
	}
}