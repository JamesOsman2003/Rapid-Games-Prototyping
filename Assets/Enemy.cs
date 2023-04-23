using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Tower;
    public float speed = 5;
    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject EnemySpawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Tower.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();
        rb.rotation = -angle;
        movement = direction;

        if (Vector2.Distance(transform.position, Tower.transform.position) < 1)
        {
            //Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        --EnemySpawner.GetComponent<EnemySpawner>().Enemycount;
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
