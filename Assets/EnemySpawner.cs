using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Tower;
    public GameObject Enemy;
    //Location
    private Vector3 alp;
    private float angle = 0;
    public float radius = 4.0f;
    //Delay
    public bool Spawning = true;
    private float Spawndelay = 0;
    public float SpawnRate = 1;

    //Enemys might be overloading the screen as such i will add a max count to number of enemyies;
    public int MaxEnemies = 10;
    public float Enemycount;

    // Update is called once per frame
    void Update()
    {
       
        Vector3 alp = Tower.transform.position;
        angle = Random.Range(0f, Mathf.PI);

        alp.x = alp.x + (Mathf.Cos(angle) * radius);
        alp.y = alp.y + (Mathf.Sin(angle) * radius);

        //Delay to stop the game from spawinng every tick
        if (Enemycount < MaxEnemies)
        {
            if (Spawning == true)
            {
                Spawndelay += Time.deltaTime;

                if (Spawndelay >= SpawnRate)
                {
                    Spawning = false;
                    Spawndelay = 0;
                }
            }
            else if (Spawning == false)
            {
                Instantiate(Enemy, alp, transform.rotation);
                Spawning = true;
                Enemycount = Enemycount + 1; 
            }
        }

    }
}
