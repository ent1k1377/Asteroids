using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private BulletShip _bulletPrefab;
    [SerializeField] private Transform _spawnBullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Transform _poolSpawn;

    private Rigidbody2D _rigidbody;
    private float _delayBetweenShoots = 0.2f;
    private float _timeAfterShoot;
    private PoolMono<BulletShip> _poolBullets;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _poolBullets = new PoolMono<BulletShip>(_bulletPrefab, _poolCount, _poolSpawn.transform);
        _poolBullets.AutoExpand = _autoExpand;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        _timeAfterShoot += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timeAfterShoot >_delayBetweenShoots)
        {
            
            BulletShip bullet = _poolBullets.GetFreeElement();
            bullet.transform.position = _spawnBullet.position;
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce((Vector2)transform.up * _bulletSpeed + _rigidbody.velocity);
            _timeAfterShoot = 0;
        }
    }
}
