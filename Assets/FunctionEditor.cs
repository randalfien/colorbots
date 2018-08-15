using UnityEngine;
using UnityEngine.UI;

public class FunctionEditor : MonoBehaviour {

	public delegate void OnTextChangeDelegate(string code);

	public event OnTextChangeDelegate OnTextChange;

	private string _text;
	
	public void HandleTextChange(string s)
	{
		_text = s;
	}

	private void OnEnable()
	{
		Overlay.Instance.Show();
	}
	
	public void Hide()
	{
		OnTextChange?.Invoke( GetComponentInChildren<InputField>().text );
		gameObject.SetActive(false);
		Overlay.Instance.Hide();
	}

	public void Open(string code)
	{
		gameObject.SetActive(true);
		_text = code;
		GetComponentInChildren<InputField>().text = code;
	}
}
