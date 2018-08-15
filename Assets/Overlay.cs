using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
	public static Overlay Instance;
	
	void Awake ()
	{
		Instance = this;
		transform.localScale = Vector3.one;		
		Deactivate();
	}

	public void Show()
	{
		gameObject.SetActive(true);	
		GetComponent<Image>().DOFade(0.75f, 0.3f);
	}

	public void Hide()
	{
		GetComponent<Image>().DOFade(0, 0.3f).OnComplete(Deactivate);
	}

	private void Deactivate()
	{
		gameObject.SetActive(false);
	}
}
