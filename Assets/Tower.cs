using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject EnemySpawner;

    public TowerHealthBar_UI healthBar;

    public float Health = 100;
    public float MaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(Health);
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(Health);
        if (Health <= 0)
        {
            EnemySpawner.GetComponent<EnemySpawner>().GameOver = true;
        }
    }
}
