using UnityEngine;

public class BotMover : MonoBehaviour
{
	public EquationObject FunctonX = new EquationObject("2*t");
	public EquationObject FunctonY = new EquationObject("sin(t)");

	private EquationObject _lastFx;
	private EquationObject _lastFy;

	public float Rotation = 0f;

	private Vector2 _startPos;

	public bool RepositioningON;
	
	private float _time;

	private Vector2 _currentVel;

	public bool Moving { get; set; } = true;

	public void OnFunctionsChange()
	{
		_lastFx = FunctonX;
		_lastFy = FunctonY;
		RepositioningON = true;
		_startPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}

	public void Move()
	{
		if (!Moving)
		{
			return;
		}

		if (FunctonX != _lastFx || FunctonY != _lastFy)
		{
			OnFunctionsChange();
		}
		
		var fi = Rotation * Mathf.PI / 180f;
		
		var dest = _startPos + new Vector2(FunctonX.Eval(_time), FunctonY.Eval(_time));

		var dt = DeltaTime;
		_time += dt;

		var d = new Vector2(dest.x,dest.y);
		dest.x = d.x * Mathf.Cos(fi) - d.y * Mathf.Sin(fi);
		dest.y = d.x * Mathf.Sin(fi) + d.y * Mathf.Cos(fi);

		dest.x = Clamp(dest.x, -ColorCanvas.UnitLimit.x, ColorCanvas.UnitLimit.x);
		dest.y = Clamp(dest.y, -ColorCanvas.UnitLimit.y, ColorCanvas.UnitLimit.y);

		var currentPos = transform.localPosition;
		if (RepositioningON && Vector2.Distance(currentPos, dest) > 0.05f)
		{
			transform.localPosition = Vector2.SmoothDamp(currentPos, dest, ref _currentVel,  0.5f, 5f, dt);
			_time -= dt;
			return;
		}

		RepositioningON = false;
		transform.localPosition = dest;
	}

	private float Clamp(float f, float min, float max)
	{
		if (f > min && f < max) return f;

		while (f < min)
		{
			f += 2 * max;
		}
		while (f > max)
		{
			f -= 2 * max;
		}

		return f;
	}

	private float DeltaTime
	{
		get
		{
			var dt = Time.deltaTime;
			return dt > 0.018f ? 0.018f : dt;
		}
	}

	public void RestartPosition()
	{
		OnFunctionsChange();
		_time = 0;
	}
}