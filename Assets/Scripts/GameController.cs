using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Trees & Coins must move at the same speed.
    private const float StaticObjectSpeed = 2f;

    // Other variable for the game
    public float PlayerJumpForce = 60f;
    public float Horizontalspeed = 3f;
    public float GameGravityModifier = 12f;
    public float RoadSpeed = StaticObjectSpeed;
    public float VehiculeSpeed = StaticObjectSpeed + 1f;
    public float OutOfScreen = -45f;
    public float RoadLeftBoundary = -1.2f;
    public float RoadRightBoundary = 0.8f;
    public float RoadFloorBoundary = 0.7f;
    public float RoadFloorCeiling = 2f;


    // Coins Collected
    private int _totalCoins = 0;

    // Game Over State
    private bool _isGameOver = false;
    
    // Helpers to check Game status
    public bool IsGameOver { get { return _isGameOver; } }
    public bool IsNotGameOver { get { return !IsGameOver; } }

    // Helper to End the Game
    public void SetGameOver()
    {
        if (IsNotGameOver)
        {
            _isGameOver = true;
            Debug.Log($"** GAME OVER ** Total coins collected #{_totalCoins} **");
        }
    }

    // 
    public void NewCoinCollected()
    {
        if (IsNotGameOver)
        {
            _totalCoins++;
            Debug.Log($"You scored a Point! $$ Coins collected #{_totalCoins} $$");
        }
    }
}
