using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCanvas : MonoBehaviour
{
	public const int TexH = 512;

	public static int TexW;

	public static Vector2 UnitSize;

	public static Vector2 UnitLimit;

	public Texture2D Texture { get; private set; }

	private Vector2 _resolution;

	// Use this for initialization
	void Start()
	{
		_resolution = new Vector2(Screen.width, Screen.height);
		UnitSize = new Vector2(10,10);
		UpdateTextureSize();
	}

	private void Update()
	{
		// ReSharper disable CompareOfFloatsByEqualityOperator
		if (_resolution.x != Screen.width || _resolution.y != Screen.height)
		{
			_resolution.x = Screen.width;
			_resolution.y = Screen.height;
			UpdateTextureSize();
		}
	}

	private void UpdateTextureSize()
	{
		var scaleX = _resolution.x / _resolution.y;
		transform.localScale = new Vector3(-1 * scaleX, 1, -1);
		
		TexW = (int) (TexH * scaleX);
		UnitSize.x = (int) UnitSize.y * scaleX;
		UnitLimit = new Vector2( UnitSize.x * 0.45f, UnitSize.y * 0.45f );


		Texture = new Texture2D(TexW, TexH, TextureFormat.ARGB32, false);
		print($"Creating texture {TexW}x{TexH} px");
		Texture.filterMode = FilterMode.Point;
		// Reset all pixels color to transparent
		Color32 resetColor = new Color32(0, 0, 0, 0);
		Color32[] resetColorArray = Texture.GetPixels32();

		for (int i = 0; i < resetColorArray.Length; i++)
		{
			resetColorArray[i] = resetColor;
		}

		Texture.SetPixels32(resetColorArray);
		Texture.Apply();

		var render = GetComponent<Renderer>();
		render.material.mainTexture = Texture;
	}
}