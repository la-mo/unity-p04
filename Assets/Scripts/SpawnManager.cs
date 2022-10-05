using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private enum SpawnEvent {
        NoSpawn = 0,
        ImpossibleAndOrCoin = 1,
        AvoidableAndOrCoin = 2
    };

    // Game Controller
    private GameController _gameController;

    // Array of 3 spawn events: [1: no spawn, 2: impossible obstacle/coin; 3: avoidable obstacle/coin]
    private SpawnEvent[] _spawnEvents = { SpawnEvent.NoSpawn, SpawnEvent.ImpossibleAndOrCoin, SpawnEvent.AvoidableAndOrCoin };
    // Array of 2 lanes: [1: left; 2: right]
    private int[] _lanes = { 1, 2 };
    // Array of possible impossible to jump obstacle to spawn
    public GameObject[] _impossibleObstaclesToSpawn;

    // Array of possible avoidable obstacle to spawn (either by jumping or rolling)
    public GameObject[] _avoidableObstaclesToSpawn;

    // Array of coins to spawn
    public GameObject[] _coinsToSpawn;

    private Vector3 _originalPosition;
    private Vector3 _lane1ObstaclePosition;
    private Vector3 _lane1CoinPosition;
    private Vector3 _lane2ObstaclePosition;
    private Vector3 _lane2CoinPosition;


    private void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Getting the original positions of the barckground to reset it to its original position.
        _originalPosition = transform.position;

        _lane1ObstaclePosition = new Vector3(-0.7f, _originalPosition.y, _originalPosition.z);
        _lane1CoinPosition = new Vector3(-0.7f, 1, _originalPosition.z);
        _lane2ObstaclePosition = new Vector3(0.3f, _originalPosition.y, _originalPosition.z);
        _lane2CoinPosition = new Vector3(0.3f, 1, _originalPosition.z);

        // Start spawning
        Invoke(nameof(SpawnObject), 2f);

    }

    // Spwan and object and call itself if the game is not over.
    void SpawnObject()
    {
        // Stop when game is over
        if (_gameController.IsGameOver) return;

        // Do we want a random position on the Y axis?
        //if (IsRandomY)
        //    _spawnPosition.y = Random.Range(0, _originalY);

        // Select which lane: 1 or 2
        // - Obstacle and Coins are not spawned on the same lane;
        Vector3 _spawnObstaclePosition;
        Vector3 _spawnCoinPosition;
        int whichLane = _lanes[Random.Range(0, _lanes.Length)];
        switch(whichLane)
        {
            default:
            case 1: // Left lane
                _spawnObstaclePosition = _lane1ObstaclePosition;
                _spawnCoinPosition = _lane2CoinPosition;
                break;
            case 2: // Right lane
                _spawnObstaclePosition = _lane2ObstaclePosition;
                _spawnCoinPosition = _lane1CoinPosition;
                break;
        }

        // Select which obstacle to random spawn:
        //  0: no spawn
        //  1: impossible obstacles
        //  2: avoidable obstacles
        SpawnEvent whichObstacleToSpawn = _spawnEvents[Random.Range(0, _spawnEvents.Length)];
        switch(whichObstacleToSpawn)
        {
            default:
            case SpawnEvent.NoSpawn : // no object to spawn
                break;
            case SpawnEvent.ImpossibleAndOrCoin:
            {
                // Select a random object to spawn
                GameObject randomObstacle = _impossibleObstaclesToSpawn[Random.Range(0, _impossibleObstaclesToSpawn.Length)];
                // Instantiate the object
                Instantiate(randomObstacle, _spawnObstaclePosition, randomObstacle.transform.rotation);
                break;
            }
            case SpawnEvent.AvoidableAndOrCoin:
            {
                // Select a random object to spawn
                GameObject randomObstacle = _avoidableObstaclesToSpawn[Random.Range(0, _avoidableObstaclesToSpawn.Length)];
                // Instantiate the object
                Instantiate(randomObstacle, _spawnObstaclePosition, randomObstacle.transform.rotation);
                break;
            }
        }

        // Select which coin to random spawn:
        //  0: no spawn
        //  1 & 2: coins
        SpawnEvent coinToSpawn = _spawnEvents[Random.Range(0, _spawnEvents.Length)];
        switch(whichObstacleToSpawn)
        {
            default:
            case SpawnEvent.NoSpawn: // no object to spawn
                break;
            case SpawnEvent.ImpossibleAndOrCoin:
            case SpawnEvent.AvoidableAndOrCoin:
            {
                // Select a random object to spawn
                GameObject randomObstacle = _coinsToSpawn[Random.Range(0, _coinsToSpawn.Length)];
                // Instantiate the object
                Instantiate(randomObstacle, _spawnCoinPosition, randomObstacle.transform.rotation);
                break;
            }
        }
        // Random time for the next spawning object
        float randomTime = Random.Range(2f, 4f);
        Invoke(nameof(SpawnObject), randomTime);
    }
}
