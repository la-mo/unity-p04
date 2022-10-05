using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    // Game Controller
    private GameController _gameController;

    private float _rotation = 10 * 20;
    private float _speed;
    private float _outOfScreen;

    private void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Getting the speed for this game
        _speed = _gameController.ObstaclesAndCoinsSpeed;
        _outOfScreen = _gameController.OutOfScreen;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin the coin
        transform.Rotate(Vector3.up * Time.deltaTime * _rotation);

        // Stop moving when game is over (but not the spin)
        if (_gameController.IsGameOver) return;

        // Move the coin back in relation to the world
        transform.Translate(Vector3.back * Time.deltaTime * _speed, Space.World);
        if (transform.position.z < _outOfScreen)
        {
            Destroy(gameObject);
        }
    }
}
