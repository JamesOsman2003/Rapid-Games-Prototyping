using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Destination
    public GameObject Tower; // Target 
    private float DistancetoTower; // Distance Check

    // Rigidbody to move using Physics
    private Rigidbody2D rb;
    private Vector2 movement;

        // Speed
        public float speed = 5; // Speed to move at

    // Spawner
    public GameObject EnemySpawner; // Used to keep track of enemy count.

    // Enemy Stats
    public int EnemyMaxHealth = 100; // Max Health
    public float EnemyHealth; // Current Health
    private float LastFrameHealth; // Previous Health

    // Health Bar
    public HealthBar healthBar; // Health Bar

    // Called Once at Start
    void Start()
    {
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner"); // Ensures that the Enemy Spawner is set within the enemy so when it gets destroyed it updates the enemy count as required.       
        EnemyHealth = EnemyMaxHealth; // Ensures that Health equals Max health when spawned
        LastFrameHealth = EnemyMaxHealth; // Sets the Default Last Health Value
        healthBar.SetMaxHealth(EnemyMaxHealth); // Sets the Health Bar Max value
        healthBar.SetHealth(EnemyMaxHealth); // Sets the Health Bar Current Value
        rb = GetComponent<Rigidbody2D>(); // Rigidbody used for movement 
         
    }

    // Update is called once per frame
    void Update()
    {
        // Direction
        Vector3 direction = Tower.transform.position - transform.position; // Gets the direction towards the tower
        direction.Normalize(); // Ensures that the direction is in the correct format

        /* Rotation - This would rotate the Health Bar as it is attached to the Enemy As Such shouldnt be used.
        // Rotation
        // float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; // Changes the angle from being in radian to being in degrees;
        // rb.rotation = -angle; // rotates the enemy as need to ensure it faces towards direction of movement
        */

        // Movement
        movement = direction; // movemnt direction 

        // Distance to Tower
        DistancetoTower = Vector2.Distance(transform.position, Tower.transform.position); // gets the distance to tower
        Mathf.Abs(DistancetoTower); // ensures that the value if negative is made positive e.g.     -1 < X < 1  ||   X < 1 

        if (DistancetoTower < 1) // checks that the distance is less than 1
        {
            Destroy(gameObject); // if distance is less than 1 it destorys enemy
            Debug.Log("2 Close too Tower");
        }

        // Health
        if (EnemyHealth != LastFrameHealth) // If Health changes Enemy Health wont equal Last Frame Health
        {
            healthBar.SetHealth(EnemyHealth); // Sets the Health Bar Value to be the correct current value
            LastFrameHealth = EnemyHealth; //Sets the Last Health to be the Current Health
        }

        if (EnemyHealth <= 0) // If Health is less or equal to 0 then enemy is dead so destroy
        {
            Destroy(gameObject); // Destroys Enemy
            Debug.Log("Enemy Health = " + EnemyHealth);
        }

    }

    private void OnDestroy()
    {
        if (EnemySpawner != null)
        {
            --EnemySpawner.GetComponent<EnemySpawner>().Enemycount; // Adjust the Enemy Count by 1 when destroyed
            ++EnemySpawner.GetComponent<EnemySpawner>().EnemyKillCount;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement); // Moves the Enemy
    }
    void MoveCharacter(Vector2 direction)
    {
        // Moves the Enemy from the current location to the desired location over time 
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
