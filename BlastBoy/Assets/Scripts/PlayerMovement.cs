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
    bool canJump;
    bool jump;
    public float baseJumpVel;
    public float jumpVel;
    public float maxJumpVel;
    int jumpCounter;
    bool canBurst;
    bool burst;
    public float burstVel;

    public Color defaultColor;
    public Color nightColor;

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

    void Update()
    {
        //Initial Jump
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                jump = true;
            }
        }

        //Burst Jump
        if (!grounded && !canJump && canBurst)
        {
            if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                burst = true;
            }
        }

        //When you let go of the jump buttons, make jump false and fall
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)))
        {
            jump = false;
            jumpCounter = 0;
        }
    }


    void FixedUpdate()
    {
        Grounded();

        //Movement Code
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

        //Jump and check if the button is still being held to vary jumps
        if (jump)
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)))
            {
                switch (jumpCounter)
                {
                    case 0:
                        break;
                    case 1:
                        jumpVel += 1;
                        break;
                    case 2:

                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        jumpVel += 3;
                        break;
                    case 6:
                        jump = false;
                        break;
                }
                jumpCounter++;
            }
            vel.y = jumpVel;
        }

        if (jumpVel > maxJumpVel)
        {
            jumpVel = maxJumpVel;
        }

        if (burst)
        {
            vel.y = burstVel;
            burst = false;
            canBurst = false;
        }

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
            canJump = true;
            jumpCounter = 0;
            jumpVel = baseJumpVel;
            canBurst = true;
        }
        else
        {
           vel.y += gravity;
            canJump = false;
        }
    }
}
