using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public GameObject TurretCP;
    public float speed = 5;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float angle;

    private float KillDelay = 0;
    private float KillRate = 4;

    public GameObject EnemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - TurretCP.transform.position;
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();
        rb.rotation = -angle;
        movement = direction;
        KillDelay += Time.deltaTime;
        if (KillDelay > KillRate)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy (gameObject);
        }
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
