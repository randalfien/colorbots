using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

	public Sprite PlaySprite;

	public Sprite PauseSprite;

	private Image _image;
	
	// Use this for initialization
	void Start () {
		GameManager.OnPlayStateChange += OnOnPlayStateChange;
		_image = GetComponent<Image>();
		OnOnPlayStateChange(GameManager.SimulationRunning);
		
		GetComponent<Button>().onClick.AddListener(OnButtonClicked);
	}

	private void OnOnPlayStateChange(bool state)
	{
		_image.sprite = state ? PauseSprite : PlaySprite;
	}

	private void OnButtonClicked()
	{
		GameManager.SimulationRunning = !GameManager.SimulationRunning;
	}
	
}
