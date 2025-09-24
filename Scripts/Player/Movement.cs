using Godot;
using System;

public partial class Movement : RigidBody3D
{
    [Export]
    private float _moveSpeed;

    private Direction _currentDirection;

    public Direction currentDirection
    {
        get { return _currentDirection; }
    }

    public void MoveInDirection(Vector3 direction, double delta)
    {
        this.SetAxisVelocity(direction * _moveSpeed * (float)delta);
        

        if (direction.X > 0)
        {
            _currentDirection = Direction.left;
        }
        else
        {
            _currentDirection = Direction.right;
        }
    }
}
