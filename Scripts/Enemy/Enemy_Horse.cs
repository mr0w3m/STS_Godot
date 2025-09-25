using Godot;
using System;

/// <summary>
/// WIP
/// </summary>
public partial class Enemy_Horse : Enemy_Base
{
    [Export] private CollisionChecker _playerCollisionCheck;

    [Export] private bool _playerFound;
    private Node3D _playerReference;
    [Export] private float _attackRange = 4f;
    [Export] private float _baseMovementSpeed = 50f;
    [Export] private float _chargeMovementSpeed = 200f;

    [Export] private float _chargeTimeSet = 2f;
    [Export] private float _chargeTimer;

    [Export] private float _chargeMoveTimeSet = 2f;
    [Export] private float _chargeMoveTimer;

    private int _chargeState = 0;


    public override void _Ready()
    {
        base._Ready();
        _playerCollisionCheck.Collided += FoundPlayer;
        _playerCollisionCheck.ExitedCollider += NoMorePlayer;
    }

    public override void _Process(double delta)
    {
        //if we see player, stay away from them. but also, update our spawn timer
        if (_playerFound && _playerReference != null)
        {
            Vector3 directionToPlayer = _playerReference.GlobalPosition - _movement.GlobalPosition;

            
            if (directionToPlayer.Length() <= _attackRange)
            {
                if (_chargeState == 0)
                {
                    //charge up
                    _movement.StopMovement();
                    if (_chargeTimer > 0)
                    {
                        _chargeTimer -= (float)delta;
                    }
                    else
                    {
                        _chargeState = 1;
                        _chargeTimer = _chargeTimeSet;
                    }
                }
                else if (_chargeState == 1)
                {
                    //run in direction for x seconds
                    _movement.OverrideMovementSpeed(_chargeMovementSpeed);
                    _movement.MoveInDirection(directionToPlayer.Normalized());
                }
            }
            else
            {
                _movement.OverrideMovementSpeed(_baseMovementSpeed);
                _movement.MoveInDirection(directionToPlayer.Normalized());
            }
        }
    }

    private void FoundPlayer(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            _playerReference = body;
            _playerFound = true;
        }
    }

    private void NoMorePlayer(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            _playerFound = false;
            _playerReference = null;
        }
    }
}
