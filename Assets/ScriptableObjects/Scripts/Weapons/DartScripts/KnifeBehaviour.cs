using UnityEngine;

public class KnifeBehaviour : WeaponManager
{
    public PlayerMovement PlayerMovement;
    private float WeaponDirectionX;
    private float WeaponDirectionY;
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();

        // Find and store the PlayerMovement component
        PlayerMovement = FindObjectOfType<PlayerMovement>();

        // Get the SpriteRenderer component attached to this GameObject
        _spriteRenderer = GetComponent<SpriteRenderer>();
      

        // Initialize WeaponDirectionX and WeaponDirectionY with PlayerMovement input values
        WeaponDirectionX = PlayerMovement._inputValueX;
        WeaponDirectionY = PlayerMovement._inputValueY;

        // If both input values are approximately 0, set WeaponDirectionX to 1
        if (Mathf.Approximately(WeaponDirectionX, 0) && Mathf.Approximately(WeaponDirectionY, 0))
        {
            WeaponDirectionX = 1;
        }

        // Initialize _destroy to true
        _destroy = true;
    }

    protected override void Update()
    {
        // Call the Attack method
        Attack();

        // If _destroy is true, increment TimeBeforeDestroy with deltaTime
        if (_destroy) TimeBeforeDestroy += Time.deltaTime;

        // If TimeBeforeDestroy exceeds TimeBeforeDestruction, call the Destroy method
        if (TimeBeforeDestroy > WeaponData.TimeBeforeDestruction) Destroy();
    }

    protected override void Attack()
    {
        // Calculate the movement vector based on WeaponDirectionX and WeaponDirectionY
        Vector3 movement = new Vector3(WeaponDirectionX * (WeaponData.speed * Time.deltaTime),
            WeaponDirectionY * (WeaponData.speed * Time.deltaTime), 0);

        // Update the position of the GameObject
        transform.position += movement;

        // Calculate rotation angle based on movement direction
        float angle = Mathf.Atan2(WeaponDirectionX, WeaponDirectionY) * Mathf.Rad2Deg;


        // If WeaponDirectionY is not zero, flip the sprite vertically


        if (WeaponDirectionY != 0)
        {
            _spriteRenderer.flipY = false;
            if (WeaponDirectionX != 0)
            {
                angle *= -1;
            }
        }
        
        

        // Apply rotation to the object on the z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void Destroy()
    {
        // Destroy the GameObject
        Destroy(gameObject);
    }
}