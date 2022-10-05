using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Game Controller
    private GameController _gameController;

    // Array of 2 lanes
    private int[] _lanes = { 1, 2};
    // Array of possible impossible to jump obstacle to spawn
    public GameObject[] _impossibleObstaclesToSpawn;

    // Array of possible avoidable obstacle to spawn (either by jumping or rolling)
    public GameObject[] _avoidableObstaclesToSpawn;

    // Array of coins to spawn
    public GameObject[] _coinsToSpawn;

    private Vector3 _originalPosition;
    private Vector3 _lane1Position;
    private Vector3 _lane2Position;
    //private float _originalZ;


    private void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Getting the original positions of the barckground to reset it to its original position.
        _originalPosition = transform.position;

        _lane1Position = new Vector3(-0.7f, _originalPosition.y, _originalPosition.z);
        _lane2Position = new Vector3(0.3f, _originalPosition.y, _originalPosition.z);

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
        Vector3 _spawnPosition;
        int whichLane = _lanes[Random.Range(0, _lanes.Length)];
        switch(whichLane)
        {
            default:
            case 1: // Left lane
                _spawnPosition = _lane1Position;
                break;
            case 2: // Right lane
                _spawnPosition = _lane2Position;
                break;
        }

        // Select what to random spawn:
        //  0: no spawn
        //  1: impossible obstacles
        //  2: avoidable obstacles and/or coins
        int whatToSpawn = Random.Range(0, 2);
        switch(whatToSpawn)
        {
            default:
            case 0: // no object to spawn
                break;
            case 1:
            {
                // Select a random object to spawn
                GameObject randomObstacle = _impossibleObstaclesToSpawn[Random.Range(0, _impossibleObstaclesToSpawn.Length)];
                // Instantiate the object
                Instantiate(randomObstacle, _spawnPosition, randomObstacle.transform.rotation);
                break;
            }
            case 2:
            {
                // Select a random object to spawn
                GameObject randomObstacle = _avoidableObstaclesToSpawn[Random.Range(0, _avoidableObstaclesToSpawn.Length)];
                // Instantiate the object
                Instantiate(randomObstacle, _spawnPosition, randomObstacle.transform.rotation);
                break;
            }
        }

        // Random time for the next spawning object
        float randomTime = Random.Range(2f, 4f);
        Invoke(nameof(SpawnObject), randomTime);
    }
}
