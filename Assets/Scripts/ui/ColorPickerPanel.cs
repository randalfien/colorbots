using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPickerPanel : MonoBehaviour
{
	public delegate void OnColorChangeDelegate(Color clr);
	
	public ColorPallete pallete;

	private GameObject itemPrefab;

	private List<GameObject> items = new List<GameObject>(6);

	public event OnColorChangeDelegate OnColorSelected;

	private void Awake()
	{
		itemPrefab = transform.GetChild(0).gameObject;
		itemPrefab.SetActive(false);
	}

	private void OnEnable()
	{
		if (!pallete)
		{
			throw new Exception("No pallete");
		}
		
		Overlay.Instance.Show();

		while (items.Count > 0)
		{
			var it = items[0];			
			items.RemoveAt(0);
			Destroy(it);
		}
		items.Clear();
		
		foreach (var clr in pallete.colors)
		{
			var item = Instantiate(itemPrefab, transform);
			item.GetComponent<Image>().color = clr;
			item.SetActive(true);
			InitButton(item, clr);
			items.Add(item);
		}

		var rect = GetComponent<RectTransform>();
		rect.sizeDelta = new Vector3(90f * pallete.colors.Length, rect.sizeDelta.y);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
		Overlay.Instance.Hide();
	}

	private void InitButton(GameObject item, Color clr)
	{
		var btn = item.GetComponent<Button>();
		btn.onClick.AddListener(() => ColorSelected(clr));
	}

	private void ColorSelected(Color clr)
	{
		OnColorSelected.Invoke(clr);
		Hide();
	}
}