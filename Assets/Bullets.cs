using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Movement
    public GameObject TurretCP; // Directional Relavence 
    private float angle; // Rotation of Bullet to face the direction of movement

    // Rigidbody to move using Physics
    private Rigidbody2D rb;
    private Vector2 movement;

    // Speed
    public float speed = 5; // Speed  to move at

    // Destroy
    private float KillDelay = 0; // timer
    private float KillRate = 4; // Time to wait

    // Hit Enemy
    private GameObject HitEnemy;
    public float BulletDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody used for movement 
    }

    // Update is called once per frame
    void Update()
    {
        // Direction
        Vector3 direction = transform.position - TurretCP.transform.position; // Gets the direction away from the tower
        direction.Normalize(); // Ensures that the direction is in the correct format

        // Rotation
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; // Changes the angle from being in radian to being in degrees;
        rb.rotation = -angle; // rotates the Bullet as need to ensure it faces towards direction of movement

        // Movement
        movement = direction; // movement direction

        // LifeSpan
        KillDelay += Time.deltaTime; // time increase
        if (KillDelay > KillRate) // Lifespan
        {
            Destroy(gameObject); // Destroys Bullet
            Debug.Log("Bullet Life Over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // does the colliding object have the tag 
        {
            // Instant destroy
            Destroy(gameObject); // Destroys the bullet on contact
            
            // Damage
            HitEnemy = collision.gameObject; // Sets the Enemy to be hit to take damage off when bullet is destroyed to prevent multiple damage points for individual attack.
        }
        if (collision.gameObject.tag == "HealthPickUp")
        {
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement); // Moves the Bullet
    }
    void MoveCharacter(Vector2 direction)
    {
        // Moves the Bullet from the current location over time 
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnDestroy()
    {
        if (HitEnemy != null) // If Bullet has Hit an Enemy this will != null 
        {
            HitEnemy.GetComponent<Enemy>().EnemyHealth -= BulletDamage; // Enemy Hit Takes Damage
            Debug.Log("Enemy Hit by Bullet");
        }
    }
}
    
