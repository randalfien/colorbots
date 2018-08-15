using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FunctionSelectPanel : MonoBehaviour
{
	public FunctionEditor FuncEditor;
	
	public delegate void OnFunctionChangeDelegate(string code);

	public event OnFunctionChangeDelegate OnFunctionSelected;

	private FunctionObject _selected;

	public Text Text;
	
	// Use this for initialization
	void Start ()
	{
		var button = GetComponentInChildren<Button>();
		button.onClick.AddListener(OnEdit);
	}

	private void FuncEditorOnOnTextChange(string code)
	{
		Text.text = code;
		OnFunctionSelected.Invoke(code);
		FuncEditor.OnTextChange -= FuncEditorOnOnTextChange;
	}

	public void OnEdit()
	{
		//show editor
		FuncEditor.Open(Text.text);
		FuncEditor.OnTextChange += FuncEditorOnOnTextChange;
	}

	public void SetFunc(string code)
	{
		Text.text = code;
	}
}
