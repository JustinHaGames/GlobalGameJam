using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 vel;

    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteRenderer sprite;

    Animator anim;

    bool grounded;

    bool inactive;

    public float accel;
    public float maxAccel;

    bool lastR;
    bool lastL;

    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        lastR = true;
        lastL = false;
        inactive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Grounded();

        bool right = Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow); 

        if (right)
        {
            vel.x += accel;
            lastL = false;
            lastR = true; 
            sprite.flipX = false;
            anim.Play("RunningAnimation");
        }

        if (left)
        {
            vel.x -= accel;
            lastL = true;
            lastR = false;
            sprite.flipX = true;
            anim.Play("RunningAnimation");
        }

        if (!left && !right)
        {
            vel.x = 0;
            anim.Play("Idle");
        }

        vel.x = Mathf.Max(Mathf.Min(vel.x, maxAccel), -maxAccel);

        rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);

    }

    void Grounded()
    {
        Vector2 pt1 = transform.TransformPoint(box.offset + new Vector2(box.size.x / 2, -box.size.y / 2));
        Vector2 pt2 = transform.TransformPoint(box.offset - (box.size / 2) + new Vector2(0, 0));

        grounded = Physics2D.OverlapArea(pt1, pt2, LayerMask.GetMask("Platform")) != null;

        if (grounded)
        {
            vel.y = 0;
        }
        else
        {
           vel.y += gravity;
           
        }
    }
}
