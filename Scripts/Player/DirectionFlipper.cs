using Godot;
using System;

public partial class DirectionFlipper : Node3D
{
    [Export]
    private Movement _movement;


    [Export] private Vector3 _leftRotation;
    [Export] private Vector3 _rightRotation;

    private Quaternion _targetRotation;
    private float _rotateSpriteSpeed = 2f;

    public override void _Process(double delta)
    {
        CheckScaleDirection();
        CheckYawDirection(delta);
    }
    
    private void CheckScaleDirection()
    {
        if (_movement.currentDirection == Direction.right)
        {
            this.Scale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.Scale = new Vector3(1, 1, 1);
        }
    }

    private void CheckYawDirection(double delta)
    {
        if (_movement.currentDirection == Direction.right)
        {
            _targetRotation = Quaternion.FromEuler(_rightRotation);
        }
        else
        {
            _targetRotation = Quaternion.FromEuler(_leftRotation);
        }

        this.Quaternion.Slerp(_targetRotation, _rotateSpriteSpeed * (float)delta);
    }
}
