using UnityEngine;

public class ScytheBehaviour : WeaponManager
{
    private float angle; // Current rotation angle of the scythe
  
    private float initialRotation; // Initial rotation of the scythe based on player input
    private float orientedAngle;
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement PlayerMovement; // Reference to the player's movement script
    private float WeaponDirectionX;
    private float WeaponDirectionY;// Oriented angle for specific rotations

    protected override void Start()
    {
        PlayerMovement = FindObjectOfType<PlayerMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        WeaponDirectionX = PlayerMovement._inputValueX;
        WeaponDirectionY = PlayerMovement._inputValueY;
        
        WeaponDirectionX = (WeaponDirectionX != 0) ? Mathf.Sign(WeaponDirectionX) : 0;
        WeaponDirectionY = (WeaponDirectionY != 0) ? Mathf.Sign(WeaponDirectionY) : 0;
        if (Mathf.Approximately(WeaponDirectionX, 0) && Mathf.Approximately(WeaponDirectionY, 0))
        {
            WeaponDirectionX = 1;
        }
       
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _destroy = true;
        angle = 0;

        // Calculate the initial rotation based on the player's movement direction
        Vector2 playerDirection = new Vector2(PlayerMovement._inputValueX, PlayerMovement._inputValueY);
        initialRotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, initialRotation);

        // Set the oriented angle based on initial rotation
        orientedAngle = CalculateOrientedAngle(initialRotation);

        // Set the initial angle for the scythe rotation
        angle = orientedAngle;
        
    }

    private float CalculateOrientedAngle(float initialRotation)
    {
        Debug.Log(initialRotation);
        // Adjust the oriented angle based on initial rotation
        switch (Mathf.RoundToInt(initialRotation))
        {
            case 0:
                return 0f;
            case 45:
                return 45f;
            case 90:
                return 90f;
            case 135:
                return 135f;
            case -45:
                return -45f;
            case -90:
                return -90f;
            case -135:
                return -135f;
            case 180:
                _spriteRenderer.flipX = true;
                return 180f;
            default:
                return 0f;
        }
    }

    protected override void Update()
    {
        base.Update();
        Attack();
        Debug.Log(WeaponDirectionY);
        Debug.Log(WeaponDirectionX);
    }

    protected override void Attack()
    {
        if (angle > -180 && transform.parent != null)
        {
            RotateScythe();
         
        }
        else
        {
            if(transform.parent != null) Destroy();
        }

        if (transform.parent == null)
        {
            Vector3 movement = new Vector3(WeaponDirectionX * (WeaponData.speed / 5 * Time.deltaTime),
                WeaponDirectionY * (WeaponData.speed /5  * Time.deltaTime), 0);

            // Update the position of the GameObject
            transform.position += movement;
            angle -= 4f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void RotateScythe()
    {
        
        // Rotate the scythe towards -180 degrees (clockwise) for a half-circle
        float targetRotation = initialRotation - 180f;
        angle = Mathf.MoveTowards(angle, targetRotation, WeaponData.speed/ 10);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    

    protected override void Destroy()
    {
        Destroy(gameObject);
    }
}
