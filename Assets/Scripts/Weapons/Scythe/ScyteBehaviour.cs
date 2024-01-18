using UnityEngine;

public class ScytheBehaviour : WeaponManager
{
    private float angle; // Current rotation angle of the scythe
    private PlayerMovement playerMovement; // Reference to the player's movement script
    private float initialRotation; // Initial rotation of the scythe based on player input
    private float orientedAngle; // Oriented angle for specific rotations

    protected override void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _destroy = true;
        angle = 0;

        // Calculate the initial rotation based on the player's movement direction
        Vector2 playerDirection = new Vector2(playerMovement._inputValueX, playerMovement._inputValueY);
        initialRotation = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, initialRotation);

        // Set the oriented angle based on initial rotation
        orientedAngle = CalculateOrientedAngle(initialRotation);

        // Set the initial angle for the scythe rotation
        angle = orientedAngle;
    }

    private float CalculateOrientedAngle(float initialRotation)
    {
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
                return 180f;
            default:
                return 0f;
        }
    }

    protected override void Update()
    {
        base.Update();
        Attack();
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
            ThrowScythe();
        }
    }

    private void RotateScythe()
    {
        // Rotate the scythe towards -180 degrees (clockwise) for a half-circle
        float targetRotation = initialRotation - 180f;
        angle = Mathf.MoveTowards(angle, targetRotation, WeaponData.speed);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ThrowScythe()
    {
        // Move the scythe based on player input
        Vector3 movement = new Vector3(playerMovement._inputValueX * WeaponData.speed /4,
            playerMovement._inputValueY * WeaponData.speed /4 , 0);

        transform.position += movement;
    }

    protected override void Destroy()
    {
        Destroy(gameObject);
    }
}
