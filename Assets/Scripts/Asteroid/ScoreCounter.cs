using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberText;

    private int _numberCoins;

    
    public void OnPlayerCoinsChanged(int value)
    {
        _numberCoins += value;
        _numberText.text = _numberCoins.ToString();
    }

    private void Start()
    {
        Asteroid asteroid = new Asteroid();
        asteroid.OnCoinsValueChangedEvent += OnPlayerCoinsChanged;
    }
}
