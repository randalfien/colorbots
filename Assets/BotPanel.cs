using UnityEngine;
using UnityEngine.UI;

public class BotPanel : MonoBehaviour
{
	public Bot Bot;

	public FunctionSelectPanel FuncX;
	public FunctionSelectPanel FuncY;
	public ColorChangerButton ColorButton;
	public Toggle ActiveToggle;
	public RotationButton RotationButton;
	public TextureButton TextureButton;
	private Camera _cam;
	private RectTransform _rect;

	public RectTransform CanvasRect;
	private Vector2 _rectSize;
	private void Awake()
	{
		_cam = Camera.main;
		_rect = GetComponent<RectTransform>();
		_rectSize = _rect.sizeDelta;
	}

	public void Show()
	{
		gameObject.SetActive(true);

		Reposition();

		FuncX.SetFunc(Bot.Mover.FunctonX.Code);
		FuncY.SetFunc(Bot.Mover.FunctonY.Code);
		ColorButton.SetColor(Bot.Painter.Color);
		RotationButton.SetRotation(Bot.Mover.Rotation);
		TextureButton.SetBrush(Bot.Painter.BrushTexture);

		ActiveToggle.isOn = Bot.Mover.Moving;
		FuncX.OnFunctionSelected += OnFuncX;
		FuncY.OnFunctionSelected += OnFuncY;
		ColorButton.OnColorChange += OnColorChange;
		TextureButton.OnTextureChange += OnTextureChange;
		RotationButton.OnRotationChange += OnRotationChange;
		ActiveToggle.onValueChanged.AddListener(a => Bot.Mover.Moving = a);//smelly
	}

	private void Reposition()
	{
		var viewport = _cam.WorldToViewportPoint(Bot.transform.position);
		viewport.x -= 0.5f;
		viewport.y -= 0.5f;
		Vector2 proportionalPosition = new Vector2(
			viewport.x * CanvasRect.sizeDelta.x,
			viewport.y * CanvasRect.sizeDelta.y
		);
         		
		_rect.localPosition = proportionalPosition;
	
		var r = _rect.anchoredPosition;
		if (r.x < _rectSize.x / 2)
		{
			r.x = _rectSize.x / 2;
		}
		if (r.x > CanvasRect.sizeDelta.x - _rectSize.x/2)
		{
			r.x = CanvasRect.sizeDelta.x -_rectSize.x / 2;
		}
		if (r.y > -_rectSize.y)
		{
			r.y = -_rectSize.y;
		}
		_rect.anchoredPosition = r;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		FuncX.OnFunctionSelected -= OnFuncX;
		FuncY.OnFunctionSelected -= OnFuncY;
		ColorButton.OnColorChange -= OnColorChange;
	}

	private void OnFuncX(string c)
	{
		Bot.Mover.FunctonX.Code = c;
	}

	private void OnFuncY(string c)
	{
		Bot.Mover.FunctonY.Code = c;
	}

	private void OnColorChange(Color c)
	{
		Bot.Painter.Color = c;
	}
	
	private void OnTextureChange(Texture2D tex)
	{
		Bot.Painter.BrushTexture = tex;
	}

	private void OnRotationChange(float rot)
	{
		Bot.Mover.Rotation = rot;
		Bot.Mover.RestartPosition();
	}
}