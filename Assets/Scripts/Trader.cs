using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private GameManager _gameManager;
    private float _currentTime;
    private TraderUI _traderUI;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _currentTime = _gameManager.currentSpeed;
        _traderUI = gameObject.GetComponent<TraderUI>();
    }

    private void Update()
    {
        // After time the trader add money to user
        if (_currentTime <= 0) {
            _gameManager.AddMoney();
            _currentTime = _gameManager.currentSpeed;
            _traderUI.ResetProgressBar();
        } else {
            _currentTime -= Time.deltaTime;
            _traderUI.UpdateProgressBar(_currentTime, _gameManager.currentSpeed);
        }
    }
}
