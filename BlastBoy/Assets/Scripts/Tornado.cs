using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    Vector2 vel;

    Rigidbody2D rb;

    public float accel;
    public float maxAccel;

    public float randomTimer;

    float timer;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomTimer = Random.Range(3, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.tornadoActive)
        {
            vel.x += accel;

            vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

            rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);

            ////Bullet Spawn
            //timer += 1 * Time.deltaTime;

            //if (timer >= randomTimer)
            //{
            //    Instantiate(bullet, transform.position, Quaternion.identity);
            //    randomTimer = Random.Range(3, 10);
            //    timer = 0;
            //}
        }
    }
}
