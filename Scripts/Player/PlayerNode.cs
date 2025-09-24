using Godot;
using System;
using System.Diagnostics;

public partial class PlayerNode : Node3D
{
    [Export] public Movement movement;

	public override void _PhysicsProcess(double delta)
	{
        ReadMovementInput(delta);
    }

    private void ReadMovementInput(double delta)
    {
        if (Input.IsActionPressed("move_left"))
        {
            movement.MoveInDirection(Vector3.Right, (float)delta);
        }

        if (Input.IsActionPressed("move_right"))
        {
            movement.MoveInDirection(Vector3.Left, (float)delta);
        }

        if (Input.IsActionPressed("move_forward"))
        {
            movement.MoveInDirection(Vector3.Back, (float)delta);
        }

        if (Input.IsActionPressed("move_backward"))
        {
            movement.MoveInDirection(Vector3.Forward, (float)delta);
        }
    }
}
