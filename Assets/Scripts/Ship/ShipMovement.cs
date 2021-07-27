using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 20;
    [SerializeField] private float _accelerationSpeed;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Acceleration();
        CheckSpeed();
    }

    private void Acceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _rigidbody.AddForce(transform.up * _accelerationSpeed * Time.deltaTime);
        }
    }

    public void Move(Vector2 boundaryScale)
    {
        transform.position *= boundaryScale;
        _rigidbody.AddForce(transform.up * _accelerationSpeed / 10);
    }

    private void CheckSpeed()
    {
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }
}
