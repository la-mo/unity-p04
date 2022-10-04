using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Trees & Coins must move at the same speed.
    private const float StaticObjectSpeed = 1f;

    // Other variable for the game
    public float PlayerJumpForce = 100f;
    public float GameGravityModifier = 15f;
    public float RoadSpeed = StaticObjectSpeed;
    public float VehiculeSpeed = StaticObjectSpeed + 1f;
    public float OutOfScreen = -45f;
    public float RoadLeftBoundary = 125f;
    public float RoadRightBoundary = 2f;


    // Coins Collected
    private int _totalCoins = 0;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
