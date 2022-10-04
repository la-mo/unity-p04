using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    private float _speed;
    private Vector3 _originalPosition;
    private float _originalZ;
    private float _lengthOfRoadTile = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the original positions of the barckground to reset it to its original position.
        _originalPosition = transform.position;
        _originalZ = _originalPosition.z;
        // Getting the speed for this game
        _speed =  2f; // _gameManager.BackgroundSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the background with respect to the world.
        // Once the background gets to a certain point, I will reset it.
        transform.Translate(Vector3.back * Time.deltaTime * _speed, Space.World);
        if (transform.position.z < (_originalZ - _lengthOfRoadTile))
        {
            // Set the Background to its original position
            transform.position = _originalPosition;
        }   
    }
}
