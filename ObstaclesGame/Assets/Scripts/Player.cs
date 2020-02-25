using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] int health = 3;
    [SerializeField] int seeds = 0;
    [SerializeField] GameObject projectileSeed;
    [SerializeField] float projectileSpeed = 10f;

    private bool isBoosting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TryShooting();
        TryToStopBoosting();
    }

    

    private void Move()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (transform.position.y < 6.0f) {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
            else {
                if (!isBoosting) {
                    FindObjectOfType<GameSession>().Boost();
                }
                isBoosting = true;
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    private void TryShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && seeds>0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectileSeed, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        seeds--;
    }

    private void TryToStopBoosting()
    {
        if (transform.position.y < 6.0f && isBoosting)
        {
            FindObjectOfType<GameSession>().StopBoost();
            isBoosting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle") || collision.gameObject.tag.Equals("FlyingObstacle"))
        {
            health--;
        }
        if (collision.gameObject.tag.Equals("Hole") || health <= 0)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag.Equals("ProjectileSeed"))
        {
            seeds++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag.Equals("HealthSeed"))
        {
            health++;
            Destroy(collision.gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetSeeds()
    {
        return seeds;
    }
}
