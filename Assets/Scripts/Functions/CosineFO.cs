using UnityEngine;
[CreateAssetMenu(fileName = "Cos", menuName = "Function/Cos")]
public class CosineFO : FunctionObject
{
	public override float Eval(float t)
	{
		return Mathf.Cos(t);
	}
}
