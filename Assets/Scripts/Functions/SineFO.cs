using UnityEngine;
[CreateAssetMenu(fileName = "Sine", menuName = "Function/Sine")]
public class SineFO : FunctionObject
{
	public override float Eval(float t)
	{
		return Mathf.Sin(t);
	}
}
