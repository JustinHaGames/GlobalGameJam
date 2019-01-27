using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    GameObject player;

    Vector3 dir;

    Rigidbody2D rb;

    Vector3 vel;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        dir = (new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z) - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        vel = dir * speed;

        rb.MovePosition(transform.position + vel * Time.deltaTime);
    }
}
