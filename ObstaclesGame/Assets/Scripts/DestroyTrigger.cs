using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle") || 
            collision.gameObject.tag.Equals("Hole") || 
            collision.gameObject.tag.Equals("FlyingObstacle")  || 
            collision.gameObject.tag.Equals("ProjectileSeed") || 
            collision.gameObject.tag.Equals("HealthSeed"))
        {
            Destroy(collision.gameObject);
        }
    }
}
