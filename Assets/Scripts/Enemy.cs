using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector3 _initialPosition;
    private int _direction;
    private float _leftBound;
    private float _rightBound;
    private bool _isWalking;

    public int Speed;
    public Vector2 Range;
    public bool EnableMovement;

    public void Start()
    {
        _initialPosition = transform.position;
        _direction = -1;
        _leftBound = _initialPosition.x - Range.x;
        _rightBound = _initialPosition.x + Range.y;
        _rigidbody = GetComponent<Rigidbody2D>();
        print("LeftBound=" + _leftBound);
        print("RightBound=" + _rightBound);
    }

    public void Update()
    {
        if (EnableMovement)
        {
            Walk();
        }
        else
        {
            StopWalking();
        }
    }

    private void StopWalking()
    {
        print("Stopping walking");
        _isWalking = false;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void Walk()
    {
        if (!EnableMovement)
        {
            return;
        }

        if (!_isWalking)
        {
            _isWalking = true;
            string direction = _direction == -1 ? "Left" : "Right";
            print("Starting walking [" + direction + "] at " + Speed + " units per second");
        }

        _rigidbody.velocity = new Vector2(Speed * _direction, _rigidbody.velocity.y);

        switch (_direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x <= _leftBound)
                {
                    print("Too far left, turning around");
                    TurnAround();
                }

                break;
            case 1:
                //Moving Right
                if (transform.position.x >= _rightBound)
                {
                    print("Too far right, turning around");
                    TurnAround();
                }

                break;
            default:
                _direction = 1;
                break;
        }
    }

    private void TurnAround()
    {
        _direction *= -1;
//        _rigidbody.velocity = new Vector2(Speed * _direction, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("Colliding with something!");
        Collider2D otherCollider = other.collider;
        IKillable killable = otherCollider.GetComponent<IKillable>();
        if (killable != null)
        {
            print("Killing something!");
            killable.Kill();
        }

        IBoundary boundary = otherCollider.GetComponent<IBoundary>();
        if (boundary != null)
        {
            print("Boundary found! IsObstacle=" + boundary.IsObstacle);
        }

        Wall wall = otherCollider.GetComponent<IBoundary>() as Wall;
        if (wall != null)
        {
            print("Collided with a wall");
            if (wall.IsObstacle)
            {
                TurnAround();
            }
        }
    }
}