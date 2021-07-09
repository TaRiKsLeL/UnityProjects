using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;
using System;

public class MyPlayerController : MonoBehaviour
{
    public int hp = 100;

    public float speed = 10f;
    public GameObject bulletPrefab;
    public Camera playerCamera;

    PhotonView photonView;

    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        gameObject.name = photonView.Owner.NickName;

        if (!photonView.IsMine)
        {
            playerCamera.enabled = false;
        }

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            LookAtMouse();
            TryToShoot();
            Move();
        }
    }

    private void LookAtMouse()
    {
        Vector3 mouseScreenPosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    private void TryToShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, transform.position, Quaternion.identity, 0);

            Vector3 shootTo = playerCamera.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("=====================");
            Debug.Log(shootTo.x - transform.position.x);
            Debug.Log(shootTo.y - transform.position.y);

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootTo.x - transform.position.x, shootTo.y - transform.position.y);
        }
    }

    private void Move()
    {

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.parent.position += move * speed * Time.deltaTime;

    }

    public void Hit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Debug.Log("Hit");
        }
    }
}
