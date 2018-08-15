using UnityEngine;

[CreateAssetMenu(fileName = "Const", menuName = "Function/Const")]
public class ConstFO : FunctionObject
{
	public float a;

	public override float Eval(float t)
	{
		return a;
	}
}
