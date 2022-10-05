using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    // Game Controller
    private GameController _gameController;

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
        _outOfScreen = _gameController.OutOfScreen;    }

    // Update is called once per frame
    void Update()
    {
        // Stop when game is over
        if (_gameController.IsGameOver) return;

        transform.Translate(Vector3.back * Time.deltaTime * _speed, Space.World);
        if (transform.position.z < _outOfScreen)
        {
            Destroy(gameObject);
        }        
    }
}
