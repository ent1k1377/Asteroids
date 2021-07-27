using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    private AsteroidSpawner _asteroidSpawner;
    private float _speed;
    private float _speedRotate;
    private Rigidbody2D _rigidbody;
    private int _numberColliderEntries;
    private float _direction;
    private int _numberChildAsteroids = 2;

    public SizeAsteroid Size;
    public delegate void AsteroidDestruction(int value);
    public event AsteroidDestruction OnCoinsValueChangedEvent;
    public enum SizeAsteroid { Small, Medium, Large };

    private void Awake()
    {
        _speed = Random.Range(50, 100);
        _speedRotate = Random.Range(20, 40);
        _rigidbody = GetComponent<Rigidbody2D>();
        _asteroidSpawner = AsteroidSpawner.instance;
    }

    private void Update()
    {
        Rotate();
    }

    public void Move()
    {
        _rigidbody.AddForce(transform.up * _speed);
        _direction = transform.localEulerAngles.z;
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, _speedRotate * Time.deltaTime);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void BoundaryEnterCounter()
    {
        _numberColliderEntries++;
        if (_numberColliderEntries == 2) // Если астероид попал в колайдер Boundary во второй раз, он деактивируется (Первый раз он попадант при спавне)
        {
            Size = SizeAsteroid.Large;
            transform.localScale = Vector3.one;
            _numberColliderEntries = 0;
            Deactivate();
        }
    }

    private void BulletHit(BulletShip bullet, Collider2D collision) // Этот метод нужно переработать, НЕ ЗАБУДЬ ОБ ЭТОМ
    {
        Vector3 scale;
        SizeAsteroid size;

        bullet.Deactivate();
        Deactivate();

        if (Size == SizeAsteroid.Small)
        {
            transform.localScale = Vector3.zero;
            Deactivate();
            return;
        }
        else if (Size == SizeAsteroid.Medium)
        {
            scale = new Vector3(0.4f, 0.4f, 0.4f);
            size = SizeAsteroid.Small;
        }
        else
        {
            scale = new Vector3(0.7f, 0.7f, 0.7f);
            size = SizeAsteroid.Medium;
            OnCoinsValueChangedEvent?.Invoke(20);
        }

        for (int i = 0; i < _numberChildAsteroids; i++)
        {
            _direction -= 45;
            Asteroid asteroid = _asteroidSpawner.CreateChildAsteroid(collision.transform.position, _direction, scale);
            asteroid.Size = size;
            asteroid.Move();
            _direction += 135;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Boundary boundary))
        {
            BoundaryEnterCounter();
        }
        if (collision.gameObject.TryGetComponent(out BulletShip bullet))
        {
            BulletHit(bullet, collision);
        }
    }

}
