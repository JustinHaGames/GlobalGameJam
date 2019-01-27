using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeMovement : MonoBehaviour
{
    Vector2 vel;

    Rigidbody2D rb;
    BoxCollider2D box;
    Animator anim;
    SpriteRenderer sprite;

    bool grounded;

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

    public GameObject negativeExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        lastR = true;
        lastL = false;
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
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

        if (GameManager.instance.inactive)
        {
            anim.Play("NegativeIdle");
        }


        if (!GameManager.instance.inactive && !GameManager.instance.blastOff)
        {
            //Movement Code
            bool left = Input.GetKey(KeyCode.RightArrow);
            bool right = Input.GetKey(KeyCode.LeftArrow);

            if (right)
            {
                vel.x += accel;
                lastL = false;
                lastR = true;
                if (grounded)
                {
                    anim.Play("NegativeRunAnimation");
                }
                if (!left)
                {
                    sprite.flipX = false;
                }
            }

            if (left)
            {
                vel.x -= accel;
                lastL = true;
                lastR = false;
                if (grounded)
                {
                    anim.Play("NegativeRunAnimation");
                }
                if (!right)
                {
                    sprite.flipX = true;
                }
            }

            if (!left && !right && grounded)
            {
                vel.x = 0;
                anim.Play("NegativeIdle");
            }

            if (right && left)
            {
                vel.x = 0;
                anim.Play("NegativeIdle");
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
                            anim.Play("NegativeJump");
                            Instantiate(negativeExplosion, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
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
                anim.Play("NegativeJump");
                Instantiate(negativeExplosion, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                burst = false;
                canBurst = false;
            }

            rb.MovePosition((Vector2)transform.position + vel * Time.deltaTime);
        }
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
            if (!GameManager.instance.inactive)
            {
                vel.y += gravity;
                canJump = false;
            }
            }
        }
}