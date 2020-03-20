using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] AudioClip pacmanChomp;


    Direction direction;

    void Start()
    {
        direction = Direction.RIGHT;
    }

    void Update()
    {
        AttemptMove();
    }

    private void AttemptMove()
    {
        float translationX = Input.GetAxis("Vertical") * speed;
        float translationZ = Input.GetAxis("Horizontal") * speed;

        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        transform.Translate(0, 0, translationZ);
        transform.Translate(-translationX, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Dot"))
        {
            SoundsManager.instance.PlaySoundWithoutInterrupt(pacmanChomp);
            Destroy(other.gameObject);
        }
    }
}

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
    MAX_DIRECTIONS

}