using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Location
    public GameObject EnemySpawnCenter; // Center Point that enemys will spawn around
    private Vector3 SpawnLocation = Vector3.zero; // Enemy Spawn Location as undefined 
    

    private float SpawnAngle = 0; // Angle of Offset that the Enemy will Spawn 
    public float SpawnRadius = 8.0f; // Distance along Angle Offset Enemy Will Spawn

    public GameObject Enemy; // Enemy To Create

    // Delay
    private float Spawndelay = 0; // increases with time to determine the time since last enemy was spawned
    public float SpawnRate = 1; // how long a gap should occur between Enemy Spawning
    public bool Spawning = true; // is Spawning?

    // Enemys might be overloading the screen as such i will add a max count to number of enemyies;

    public int MaxEnemies = 10; // Max Enemy Count 
    public float Enemycount; // Current Enemy Count

   
    // Update is called once per frame
    void Update()
    {
        // Spawn Location
        SpawnLocation = EnemySpawnCenter.transform.position; // gets start position
        
        SpawnAngle = Random.Range(0f, Mathf.PI); // gets a random value of radians between 0 and PI       
        // Sets the Spawn location using the random value with the radius determined to spawn a enemy at the desired location
        SpawnLocation.x = SpawnLocation.x + (Mathf.Cos(SpawnAngle) * SpawnRadius); // Moves as required on X axis to stay within path of circle
        SpawnLocation.y = SpawnLocation.y + (Mathf.Sin(SpawnAngle) * SpawnRadius); // Moves as required on Y axis to stay within path of circle
        
       
        //Delay to stop the game from spawinng every tick
        if (Enemycount < MaxEnemies) // if the enemy count is < Max enemies it will continue to spawn enemys 
        {
            if (Spawning == true) // enemy just been spawned
            {
                Spawndelay += Time.deltaTime; // time increase

                if (Spawndelay >= SpawnRate) // time check
                {
                    Spawning = false; // Enemy hasnt just been spawned
                    Spawndelay = 0; // timer reset
                }
            }
            else if (Spawning == false) // Enemy hasnt just been spawned
            {
                GameObject EnemySpawned = Instantiate(Enemy, SpawnLocation, transform.rotation); // Create Enemy // Enemy Spawned allows me to adjust varibles within the new object
                Spawning = true; // Enemy has just been spawned
                ++ Enemycount; // Enemy Count increases by 1
                Debug.Log("Enemy Spawned |=| Enemy Count = " + Enemycount);
            }
        }

    }
}
