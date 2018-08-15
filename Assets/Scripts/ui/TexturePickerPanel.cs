using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TexturePickerPanel : MonoBehaviour
{
	public delegate void OnTextureChangeDelegate(Texture2D tx);

	public BrushSO[] Brushes;

	private GameObject itemPrefab;

	private List<GameObject> items = new List<GameObject>(6);

	public event OnTextureChangeDelegate OnTextureSelected;

	private void Awake()
	{
		itemPrefab = transform.GetChild(0).gameObject;
		itemPrefab.SetActive(false);
	}

	private void OnEnable()
	{
		if (Brushes.Length == 0)
		{
			throw new Exception("No brushes");
		}
		
		Overlay.Instance.Show();

		while (items.Count > 0)
		{
			var it = items[0];			
			items.RemoveAt(0);
			Destroy(it);
		}
		items.Clear();
		
		foreach (var b in Brushes)
		{
			var item = Instantiate(itemPrefab, transform);
			item.GetComponent<Image>().sprite = b.brushSprite;
			item.SetActive(true);
			item.GetComponent<Image>().SetNativeSize();
			InitButton(item, b);
			items.Add(item);
		}

		var rect = GetComponent<RectTransform>();
		rect.sizeDelta = new Vector3(90f * Brushes.Length, rect.sizeDelta.y);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
		Overlay.Instance.Hide();
	}

	private void InitButton(GameObject item, BrushSO b)
	{
		var btn = item.GetComponent<Button>();
		btn.onClick.AddListener(() => ColorSelected(b));
	}

	private void ColorSelected(BrushSO b)
	{
		OnTextureSelected.Invoke(b.brushTexture);
		Hide();
	}
}