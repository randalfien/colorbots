using UnityEngine;
using UnityEngine.EventSystems;

public class AddNewBotButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public GameObject BotPrefab;

	private Camera _camera;

	private GameObject _current;

	private RectTransform _rect;
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		_rect = transform.parent.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		var p = _camera.ScreenToWorldPoint(eventData.position);
		p.z = 0;
		_current.transform.position = p;		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_current = Instantiate(BotPrefab);
		_current.GetComponent<Bot>().enabled = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_current.GetComponent<Bot>().enabled = true;
	}
}
