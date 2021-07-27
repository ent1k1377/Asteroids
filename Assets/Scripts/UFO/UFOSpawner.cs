using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOSpawner : MonoBehaviour
{
    [SerializeField] private UFO _ufo;
    [SerializeField] private Vector2 _spawnPositionY;
    [SerializeField] private Vector2 _spawnPositionX;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, Random.Range(5, 10));
    }

    private void Spawn()
    {
        Vector3 ufoPosition = _ufo.transform.position;
        if (Random.Range(1, 2) < 1.5f)
        {
            ufoPosition.x = _spawnPositionX.x;
            _ufo.Speed= -Math.Abs(_ufo.Speed);
        }
        else
        {
            ufoPosition.x = _spawnPositionX.y;
            _ufo.Speed = Math.Abs(_ufo.Speed);
        }
        
        ufoPosition.y = Random.Range(_spawnPositionY.x, _spawnPositionY.y);
        _ufo.transform.position = ufoPosition;
        _ufo.gameObject.SetActive(true);
        
        
    }
}
