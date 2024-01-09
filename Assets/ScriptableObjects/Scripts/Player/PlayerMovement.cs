using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player movement input values
    [Header("PlayerMoves")] 
    public float _inputValueY;  // Vertical input
    public float _inputValueX;  // Horizontal input

    // Player position variables
    private float _posX;
    private float _posY;

    // Player movement speed
    [SerializeField] private float speed;

    // Player movement direction vector
    public Vector3 direction;

    // References to components
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // Border constraints for player movement
    [Header("Clamp Values")]
    [SerializeField] private float BORDMINX;
    [SerializeField] private float BORDMINY;
    [SerializeField] private float BORDMAXX;
    [SerializeField] private float BORDMAXY;

    // Animator parameter hash for IsMoving
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    // Called when the script starts
    public void Start()
    {
        // Get references to Animator and SpriteRenderer components
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values from the player
        _inputValueX = Input.GetAxis("Horizontal");
        _inputValueY = Input.GetAxis("Vertical");

        // Check if the player is moving
        if (_inputValueY != 0 || _inputValueX != 0) 
            Move();  // If moving, call the Move() function
        else
        {
            // If not moving, set direction to zero and update animator
            direction = Vector3.zero;
            _animator.SetBool(IsMoving, false);
        }
    }

    // Function to handle player movement
    void Move()
    {
        // Get the current position of the player
        var position = transform.position;

        // Calculate new positions based on input and speed
        _posX = (position.x + (_inputValueX * Time.deltaTime * speed));
        _posY = (position.y + (_inputValueY * Time.deltaTime) * speed);

        // Clamp the player position inside the defined borders
        _posX = Mathf.Clamp(_posX, BORDMINX, BORDMAXX);
        _posY = Mathf.Clamp(_posY, BORDMINY, BORDMAXY);     

        // Set the new direction vector
        direction = new Vector3(_posX, _posY, 0);

        // Move the player to the new position
        transform.position = direction;

        // Update animator parameter for movement
        _animator.SetBool(IsMoving, true);

        // Flip the player sprite based on movement direction
        if (_inputValueX < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
