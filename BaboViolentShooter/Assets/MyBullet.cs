using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System;
public class MyBullet : MonoBehaviour
{
    public int damage = 15;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(directionVector * speed * Time.deltaTime)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MyPlayerController player = collision.gameObject.GetComponent<MyPlayerController>();
        if (player != null)
        {
            player.Hit(damage);
        }

        PhotonNetwork.Destroy(gameObject);
    }
}
