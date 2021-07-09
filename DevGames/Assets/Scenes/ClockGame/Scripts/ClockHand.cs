using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHand : MonoBehaviour { 

    public KeyCode keyCode = KeyCode.H;
    
    void Update()
    {
        if (Input.GetKey(keyCode))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x * 150 - transform.position.x,
                mousePosition.y * 150 - transform.position.y
                );
            transform.up = direction;
        }
    }
}
