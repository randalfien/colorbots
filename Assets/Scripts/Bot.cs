using UnityEngine;

public class Bot : MonoBehaviour
{
	public BotMover Mover;

	public BotPainter Painter;
	
	void Awake()
	{		
		Mover = GetComponent<BotMover>();
		Painter = GetComponent<BotPainter>();	
	}

	void Update()
	{
		if ( GameManager.SimulationRunning )
		{
			if (!GameManager.FastForward)
			{
				UpdateComponents();
			}
			else
			{
				for (int i = 0; i < 12; i++)
				{
					UpdateComponents();
				}
			}
		}
	}

	private void UpdateComponents()
	{
		Mover.Move();
		if (!Mover.RepositioningON)
		{
			Painter.Paint();
		}
	}
}