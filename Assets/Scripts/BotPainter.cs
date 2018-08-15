using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPainter : MonoBehaviour
{
	public Texture2D DefaultTexture;
	public Color DefaultColor;
	
	private Color _color;
	public Color Color
	{
		get { return _color; }
		set { _color = value; UpdateBrush(); }
	}

	private Texture2D _brushTexture;
	public Texture2D BrushTexture
	{
		get { return _brushTexture; }
		set { _brushTexture = value; UpdateBrush(); }
	}

	public bool Painting { get; set; } = true;
	
	private ColorCanvas _canvas;
	private Color32[] _brushColors;
	private Color32[] _brushColorsTmp;
	private int _brushSize = 16;

	private void Awake()
	{
		_canvas = FindObjectOfType<ColorCanvas>();
		_brushColorsTmp = new Color32[_brushSize*_brushSize];
		_color = DefaultColor;
		_brushTexture = DefaultTexture;
		UpdateBrush();
	}
	
	private void UpdateBrush()
	{
		_brushSize = BrushTexture.height;
		_brushColors = BrushTexture.GetPixels32();
		
		var clr = (Color32) _color;
		clr.a = 0;

		for (var i = 0; i < _brushColors.Length; i++)
		{
			_brushColors[i] = new Color32(clr.r, clr.g, clr.b, _brushColors[i].a);
		}

		if (_brushColorsTmp.Length != _brushColors.Length)
		{
			_brushColorsTmp = new Color32[_brushColors.Length];
			print("reallocating brush tmp buffer");
		}
	}

	public void Paint()
	{
		if (!Painting)
		{
			return;
		}
		
		var texture = _canvas.Texture;
		
		var dest = transform.localPosition;
		
		var x = dest.x * (ColorCanvas.TexW / ColorCanvas.UnitSize.x) + (ColorCanvas.TexW >> 1) - _brushSize / 2f;
		var y = dest.y * (ColorCanvas.TexH / ColorCanvas.UnitSize.y) + (ColorCanvas.TexH >> 1) - _brushSize / 2f;

		var cur = texture.GetPixels((int) x, (int) y, _brushSize, _brushSize);
		BlendColors(cur, _brushColors, _brushColorsTmp);

		texture.SetPixels32((int) x, (int) y, _brushSize, _brushSize, _brushColorsTmp);
		texture.Apply();
	}
	
	private void BlendColors(Color[] a, Color32[] b, Color32[] result)
	{
		for (var i = 0; i < a.Length; i++)
		{
			if (b[i].a == 0)
			{
				result[i] = a[i];
			}
			else if (a[i].a <= 0)
			{
				result[i] = b[i];
			}
			else
			{
				result[i] = Mix(a[i], b[i]);
			}
		}
	}

	private static Color32 Mix(Color32 a, Color32 b)
	{
		var al = b.a / 255f;
		var am1 = 1 - al;
		return new Color32
		{
			r = Min(a.r * am1 + b.r * al, 255f),
			g = Min(a.g * am1 + b.g * al, 255f),
			b = Min(a.b * am1 + b.b * al, 255f),
			a = Min(a.a + b.a, 255f)
		};
	}

	private static byte Min(float a, float b)
	{
		return a < b ? (byte) a : (byte) b;
	}
}
