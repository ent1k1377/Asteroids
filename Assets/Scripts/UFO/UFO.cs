using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UFO : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    [SerializeField] private BulletUFO _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _bulletSpawn;

    private Rigidbody2D _rigidbody;
    private int _numberColliderEntries;

    public float Speed { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Speed = 0.5f;
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Shoot));
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _rigidbody.velocity += new Vector2(Speed, 0) * Time.deltaTime;
    }

    public IEnumerator Shoot()
    {
        for (int i = 0; i < 25; i++) 
        {
            BulletUFO bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, _bulletSpawn);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            Vector2 lookDir = (Vector2)_ship.transform.position - bulletRigidbody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletRigidbody.AddForce((Vector2)bullet.transform.up * _bulletSpeed + _rigidbody.velocity);
            yield return new WaitForSeconds(Random.Range(2,5));
        }
        

    }
    
    private void BoundaryEnterCounter()
    {
        _numberColliderEntries++;
        if (_numberColliderEntries == 2) // Если UFO попал в колайдер Boundary во второй раз, он деактивируется (Первый раз он попадант при спавне)
        {
            _numberColliderEntries = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Boundary boundary))
        {
            BoundaryEnterCounter();
        }
    }
}
