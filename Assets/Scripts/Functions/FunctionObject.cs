using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionObject : ScriptableObject
{
	public string ReadableName;
	
	public virtual float Eval(float t)
	{
		return 0;
	}
}
