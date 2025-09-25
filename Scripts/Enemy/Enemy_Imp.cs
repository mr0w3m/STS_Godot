using Godot;
using System;
using System.Diagnostics;

public partial class Enemy_Imp : Enemy_Base
{
	[Export] private Movement _movement;
	[Export] private CollisionChecker _playerCheck;


	[Export] private bool _playerFound;
	private Node3D playerReference;
	[Export]private float _attackRange = 0.5f;

    public override void _Ready()
    {
        base._Ready();
		_playerCheck.Collided += FoundPlayer;
		_playerCheck.ExitedCollider += NoMorePlayer;
    }

	//process enemy inputs
	public override void _Process(double delta)
	{
		//check for player locally through collision check
		//if player is local, move towards player by getting a vector between the two and normalizing
		//we can figure out animations after this for attacking when we get close enough
		if (_playerFound && playerReference != null)
		{
			Vector3 directionToPlayer = (playerReference.GlobalPosition - _movement.GlobalPosition);
			if (directionToPlayer.Length() <= _attackRange)
			{
				//Debug.Print(this.Name + " is in attack range, and attacking player");
				_movement.StopMovement();
			}
			else
			{
				//move towards player
				//Debug.Print("MovingTowardsPlayer");
				_movement.MoveInDirection(directionToPlayer.Normalized());
			}
		}
		else
		{
			_movement.StopMovement();
		}
	}

	private void FoundPlayer(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
            playerReference = body;
            _playerFound = true;
        }
    }

	private void NoMorePlayer(Node3D body)
	{
        if (body.IsInGroup("Player"))
		{
            _playerFound = false;
            playerReference = null;
        }
    }
}
