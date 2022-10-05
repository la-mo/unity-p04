using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
        // Game Manager
    private GameController _gameController;
    // Jump force
    private float _jumpForce;
    private float _roadY = 0.7f;
    // Player starts on the road
    private bool _isOnTheRoad = true;
    // Screen limits
    private float _roadLeftBoundary;
    private float _roadRightBoundary;
    // Rigidbody to detect collision
    private Rigidbody _playerRb;
    // Animator
    private Animator _anim;
    private const string ANIM_RUN = "Run";
    private const string ANIM_JUMP = "Flip";
    private const string ANIM_ROLL = "Roll";
    private const string ANIM_DEATH = "Death2";

    // Audio source
    private AudioSource _playerAudio;
    public AudioClip JumpSound;
    public AudioClip ExplosionSound;
    public AudioClip CoinSound;

    // Particle system
    public ParticleSystem Splatter;
    public ParticleSystem Explosion;
    public ParticleSystem CoinPop;

    private void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _playerRb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the jump force and screen limits
        _jumpForce = _gameController.PlayerJumpForce;
        _roadLeftBoundary = _gameController.RoadLeftBoundary;
        _roadRightBoundary = _gameController.RoadRightBoundary;

        // We assume we start on tthe road;
        _roadY = transform.position.y;


        // Set gravity for the player's jump.
        Physics.gravity *= _gameController.GameGravityModifier;
        // Start running.
        _anim.SetBool(ANIM_RUN, true);
        Splatter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _jumpForce = _gameController.PlayerJumpForce;
        // Check if the conditions are met for "Jumping"
        if (Input.GetKeyDown(KeyCode.Space) && _isOnTheRoad && _gameController.IsNotGameOver)
        {
            Splatter.Stop();
            _anim.SetTrigger(ANIM_JUMP);
            _playerAudio.PlayOneShot(JumpSound);
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isOnTheRoad = false;
        }

                // Limit the player on the Y axis position
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, _roadY, 2f), 
            transform.position.z);

    }

    private void OnCollisionEnter(Collision col)
    {
        // Did the player land on the Ground? yes, then start running
        if (col.gameObject.CompareTag("Road") && _gameController.IsNotGameOver)
        {
            _isOnTheRoad = true;
            Splatter.Play();
        }

        // Did the player hit an obstacle? yes, then that is GAME OVER !
        if (col.gameObject.CompareTag("Obstacle") && _gameController.IsNotGameOver)
        {
            _gameController.SetGameOver();
            Splatter.Stop();
            Explosion.Play();
            _anim.SetTrigger(ANIM_DEATH);
            _playerAudio.PlayOneShot(ExplosionSound);
        }

        // Did the player hit a Coin? yes, then collect it!
        if (col.gameObject.CompareTag("Coin") && _gameController.IsNotGameOver)
        {
            _gameController.NewCoinCollected();
            CoinPop.Play();
            _playerAudio.PlayOneShot(CoinSound);
            Destroy(col.gameObject);
        }
    }
}
