using System.Collections.Generic;
using Jace;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
	public delegate void OnPlayStateChangeDelegate(bool state);

	public static bool FastForward = false;
	private static bool _simulationRunning = true;

	public float HoldToCreateTime = 1.2f;
	public float MaxTapTime = 0.8f;
	public float MinDragTime = 0.1f;

	public GameObject BotPrefab;
	public BotPanel BotPanel;
	public LayerMask BotLayerMask;


	private float _createBotTimer;
	private Camera _cam;
	private bool _waitingForCreate;
	private bool _touchStartOnBot;
	private bool _wasDragging;
	private GameObject _currentBot;
	private float _timeTouchStart;
	private EventSystem _events;

	public static event OnPlayStateChangeDelegate OnPlayStateChange;

	public static bool SimulationRunning
	{
		get { return _simulationRunning; }
		set
		{
			if (value != _simulationRunning)
			{
				_simulationRunning = value;
				OnPlayStateChange?.Invoke(value);
			}
		}
	}

	private void Start()
	{
		_cam = Camera.main;
		_events = EventSystem.current;
		SimulationRunning = false;
	}

	private RaycastHit2D GetBotUnderMouse()
	{
		var ray = _cam.ScreenToWorldPoint(Input.mousePosition);
		return Physics2D.Raycast(ray, Vector2.zero, BotLayerMask);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_createBotTimer = 0;
			var hit = GetBotUnderMouse();
			if (hit)
			{
				_touchStartOnBot = true;
				_waitingForCreate = false;
				_timeTouchStart = Time.fixedTime;
				_currentBot = hit.transform.gameObject;
			}
			else
			{
				var overUI = _events.IsPointerOverGameObject();
				_waitingForCreate = !overUI;
				_touchStartOnBot = false;
				if (BotPanel.gameObject.activeSelf && !overUI)
				{
					SimulationRunning = true;
					BotPanel.Hide();
				}
			}
		}

		//CLICKING
		if (_touchStartOnBot && Input.GetMouseButtonUp(0) && Time.fixedTime - _timeTouchStart < MaxTapTime )
		{
			_touchStartOnBot = false;
			var hit = GetBotUnderMouse();
			if (hit)
			{				
				OnBotClicked(hit.transform.gameObject);
			}
		}
		//CREATING
		if (_waitingForCreate && Input.GetMouseButton(0))
		{
			_createBotTimer += Time.deltaTime;
			if (_createBotTimer > HoldToCreateTime)
			{
				AddNewBot(GetMouseInWorldPos());
				_waitingForCreate = false;
			}
		}
		//DRAGGING
		if (!SimulationRunning && _touchStartOnBot && Input.GetMouseButton(0) && Time.fixedTime - _timeTouchStart > MinDragTime)
		{
			_wasDragging = true;
			var bot = _currentBot.GetComponent<Bot>();
			bot.Mover.Moving = false;
			bot.Painter.Painting = false;
			_currentBot.transform.localPosition = GetMouseInWorldPos();
		}
		if (_wasDragging && _currentBot && Input.GetMouseButtonUp(0))
		{		
			var bot = _currentBot.GetComponent<Bot>();
			bot.Mover.Moving = true;
			bot.Painter.Painting = true;
			bot.Mover.RestartPosition();
			_currentBot = null;
		}

		if (_wasDragging && Input.GetMouseButtonUp(0))
		{
			_wasDragging = false;
		}
	}

	private Vector3 GetMouseInWorldPos()
	{
		var result = _cam.ScreenToWorldPoint(Input.mousePosition);
		result.z = 0;
		return result;
	}

	private void OnBotClicked(GameObject bot)
	{
		print("bot clicked");
		BotPanel.Bot = bot.GetComponent<Bot>();
		BotPanel.Show();
		SimulationRunning = false;
	}

	private void AddNewBot(Vector3 worldPoint)
	{
		var bot = Instantiate(BotPrefab);		
		bot.transform.position = worldPoint;
		OnBotClicked(bot);
	}

	public void ToggleFastForward()
	{
		FastForward = !FastForward;
	}
}