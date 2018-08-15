using System;
using System.Collections.Generic;
using Jace;

public class EquationObject
{
	private CalculationEngine _engine = new CalculationEngine();
	Dictionary<string, double> _variables = new Dictionary<string, double>();
	
	private Func<Dictionary<string, double>, double> _formula;

	private string _code;

	public string Code { 
		get { return _code; }
		set { _code = value;
			ParseCode(); 	} 
	}
	
	public EquationObject(string code)
	{
		if (string.IsNullOrEmpty(code))
		{
			throw new Exception("heyy");
		}
		Code = code;
	}
	
	public float Eval(float t)
	{
		_variables["t"] =  t;
		return (float) _formula(_variables);
	}

	private void ParseCode()
	{
		if (string.IsNullOrEmpty(_code))
		{
			throw new Exception("heyy");
		}
		_variables.Clear();
		_variables.Add("t", 0);
		_formula = _engine.Build(_code);
		//todo?
	}
}
