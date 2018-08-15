using UnityEngine;

[CreateAssetMenu(fileName = "Linear", menuName = "Function/Linear")]
public class LinearFO : FunctionObject
{
	public float a;

	public override float Eval(float t)
	{
		return a*t;
	}
}
