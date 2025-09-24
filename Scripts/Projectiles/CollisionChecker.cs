using Godot;
using System;
using System.Diagnostics;

public partial class CollisionChecker : Area3D
{
	[Export] private int _targetLayer;


	public event Action Collided;

	private void OnCollided()
	{
		if (Collided != null)
		{
			Collided.Invoke();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnBodyEntered(Node3D body)
	{
		if (body != null)
		{
			Debug.Print(body.Name + " Just entered the trigger zone");

			PhysicsBody3D pbody = (PhysicsBody3D)body;


            if (pbody!= null)
			{
				if ((pbody.CollisionLayer & _targetLayer) != 0)
				{
					Debug.Print("Collided with layer: " + _targetLayer);
					OnCollided();
                }
			}
        }
	}
}
