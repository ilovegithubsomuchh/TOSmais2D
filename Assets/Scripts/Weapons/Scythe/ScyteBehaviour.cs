using UnityEngine;

public class ScytheBehaviour : WeaponManager
{
    private float angle;
    private int attackCounter;
    private PlayerMovement PlayerMovement;
    private float WeaponDirectionX;
    private float WeaponDirectionY;
    private GameObject player;
   

    protected override void Start()
    {
       
        player = GameObject.Find("Player");
        PlayerMovement = FindObjectOfType<PlayerMovement>();
        // Ensure the object is on the correct Z position and set initial values
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _destroy = true;
        angle = 0;
        WeaponDirectionX = PlayerMovement._inputValueX;
        WeaponDirectionY = PlayerMovement._inputValueY;
        WeaponDirectionX = (WeaponDirectionX != 0) ? Mathf.Sign(WeaponDirectionX) : 0;
        WeaponDirectionY = (WeaponDirectionY != 0) ? Mathf.Sign(WeaponDirectionY) : 0;
        if (Mathf.Approximately(WeaponDirectionX, 0) && Mathf.Approximately(WeaponDirectionY, 0))
        {
            WeaponDirectionX = 1;
        }
    }

    protected override void Update()
    {
        base.Update();
       
        Attack();
    }

    protected override void Attack()
    {
        // Check if the scythe should continue rotating
        if (angle > -140f && transform.parent != null)
        {
            RotateScythe();
        }
        if(transform.parent == null)
        {
           ThrowScythe();
        }
    }

    private void ThrowScythe()
    {
        
        Vector3 movement = new Vector3(WeaponDirectionX * (WeaponData.speed * Time.deltaTime),
            WeaponDirectionY * (WeaponData.speed * Time.deltaTime), 0);

        // Update the position of the GameObject
        transform.position += movement;
        RotateScythe();
        
    }

    private void RotateScythe()
    {
        // Rotate the scythe
        angle -= WeaponData.speed;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void Destroy()
    {
        // Destroy the object
        Destroy(gameObject);
    }
}