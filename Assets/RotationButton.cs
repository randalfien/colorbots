using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public delegate void OnRotationChangeDelegate(float rotation);

	public event OnRotationChangeDelegate OnRotationChange;

	private Vector2 _pointerPosStart;

	private float _rotationStart;
	
	// Use this for initialization
	void Start () {
		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_pointerPosStart = eventData.position;
		_rotationStart = transform.eulerAngles.z;
	}

	public void OnDrag(PointerEventData eventData)
	{
		var verticalDist = _pointerPosStart.y - eventData.position.y;
		var rot = _rotationStart + verticalDist;
		transform.localRotation = Quaternion.Euler(0,0,rot);
		OnRotationChange?.Invoke(rot);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//nothing
	}

	public void SetRotation(float rot)
	{
		transform.localRotation = Quaternion.Euler(0,0,rot);
	}
}
