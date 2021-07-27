using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid[] _asteroidPrefab;
    [SerializeField] private Vector2 _rangeDegrees;
    [SerializeField] private int _spawnAmount;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Transform _poolSpawn;

    private PoolMono<Asteroid> _poolAsteroids;

    public static AsteroidSpawner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        _poolAsteroids = new PoolMono<Asteroid>(_asteroidPrefab, _poolCount, _poolSpawn.transform);
        _poolAsteroids.AutoExpand = _autoExpand;

        InvokeRepeating(nameof(Spawn), 0, _spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            Asteroid asteroid = CreateChildAsteroid(transform.position, Random.Range(_rangeDegrees.x, _rangeDegrees.y), Vector3.one);
            asteroid.Move();
        }
    }

    public Asteroid CreateChildAsteroid(Vector2 position, float degree, Vector3 scale)
    {
        Asteroid asteroid = _poolAsteroids.GetFreeElement();
        asteroid.transform.position = position;
        asteroid.transform.rotation = Quaternion.Euler(0, 0, degree);
        asteroid.transform.localScale = scale;
        return asteroid;
    }
}
