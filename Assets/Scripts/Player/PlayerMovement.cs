using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMoves")] 
    public float _inputValueY;
    public float _inputValueX;
    private float _posX;
    private float _posY;
    [SerializeField] private float speed;
    public Vector3 direction;

    [Header("Clamp Values")]
    [SerializeField] private float BORDMINX;
    [SerializeField] private float BORDMINY;
    [SerializeField] private float BORDMAXX;
    [SerializeField] private float BORDMAXY;


    // Update is called once per frame
    void Update()
    {
        _inputValueX = Input.GetAxis("Horizontal");
        _inputValueY = Input.GetAxis("Vertical"); // Getting input Axis 
        if (_inputValueY != 0 || _inputValueX != 0) Move();
        else direction = Vector3.zero;
    }

    void Move()
    {
        var position = transform.position;
        _posX = (position.x + (_inputValueX * Time.deltaTime * speed));
        _posY = (position.y + (_inputValueY * Time.deltaTime) * speed); // Player movement thanks to axis 
        _posX = Mathf.Clamp(_posX, BORDMINX, BORDMAXX); // Clamp the player inside the map 
        _posY = Mathf.Clamp(_posY, BORDMINY, BORDMAXY);     
        direction = new Vector3(_posX, _posY, 0);
        transform.position = direction; // Move the player
    }
}