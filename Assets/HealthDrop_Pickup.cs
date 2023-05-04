using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop_Pickup : MonoBehaviour
{
    private GameObject Tower;
    public float PoshealthAdd = 100;
    private float health;
    private float healthadd;
    private float maxHealth;

    private void Start()
    {
        Tower = GameObject.FindGameObjectWithTag("Tower");
    }

    private void Update()
    {
        maxHealth = Tower.GetComponent<Tower>().MaxHealth;
        health = Tower.GetComponent<Tower>().Health;
        if (maxHealth - health >= PoshealthAdd)
        {
            healthadd = PoshealthAdd;
        }
        else
        {
            healthadd = maxHealth - health;
        }
    }

    private void OnDestroy()
    {
        Tower.GetComponent<Tower>().Health += healthadd;
    }
}
