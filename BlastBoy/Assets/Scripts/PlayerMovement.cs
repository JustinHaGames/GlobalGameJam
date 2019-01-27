using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 vel;

    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteRenderer sprite;
    AudioSource audio;

    Animator anim;

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
    int jumpTextCounter;
    bool canBurst;
    bool burst;
    public float burstVel;
    int burstCounter;
    bool superBurst;
    public float superBurstVel;

    public Color defaultColor;
    public Color nightColor;
    public Color flashingColor;
    float flashingTimer;

    public GameObject explosion;

    public AudioClip firstBurst;
    public AudioClip secondBurst;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        lastR = true;
        lastL = false;

        if (GameManager.instance.sceneID == 0)
        {
            sprite.color = nightColor;
        }

        if (GameManager.instance.sceneID == 1)
        {
            sprite.color = defaultColor;
        }

        if (GameManager.instance.sceneID == 2)
        {
            sprite.color = defaultColor;
        }

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

        if (GameManager.instance.inactive)
        {
            anim.Play("Idle");
        }

        if (!GameManager.instance.inactive) { 
        //Movement Code
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);

        if (right)
        {
            vel.x += accel;
            lastL = false;
            lastR = true;
            if (grounded)
            {
                anim.Play("RunningAnimation");
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

                anim.Play("RunningAnimation");
            }
            if (!right)
            {
                sprite.flipX = true;
            }
        }

        if (!left && !right && grounded)
        {
            vel.x = 0;
            anim.Play("Idle");
        }

        if (right && left)
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
                        anim.Play("Jump");
                        if (!GameManager.instance.blastOff)
                        {
                            audio.PlayOneShot(firstBurst, 1f);
                        }
                        jumpTextCounter += 1;
                        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
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
            anim.Play("Jump");
            if (!GameManager.instance.blastOff)
            {
                audio.PlayOneShot(secondBurst, 1f);
            }
            burstCounter += 1;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
            burst = false;
            canBurst = false;
        }

        if (GameManager.instance.sceneID == 0)
        {

            if (jumpTextCounter == 1)
            {
                GameManager.instance.discoverJump = true;
            }
            else
            {
                GameManager.instance.discoverJump = false;
            }

            if (burstCounter == 0)
            {
                if (vel.y < 0)
                {
                    Time.timeScale = .25f;
                }
            }
            else
            {
                Time.timeScale = 1f;
            }
            if (burstCounter >= 3)
            {
                superBurst = true;
            }
        }

        if (GameManager.instance.sceneID == 2)
        {
            if (burstCounter >= 7f)
            {
                superBurst = true;
            }
        }

        if (superBurst)
        {
            flashingTimer += 1 * Time.deltaTime;
            if (flashingTimer <= .5f)
            {
                sprite.color = flashingColor;
            }
            else if (flashingTimer > .5f && flashingTimer < 1f)
            {
                if (GameManager.instance.sceneID == 0)
                {
                    sprite.color = nightColor;
                }
                else
                {
                    sprite.color = defaultColor;
                }
            }
            else if (flashingTimer >= 1f)
            {
                flashingTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                vel.y = superBurstVel;
                GameManager.instance.blastOff = true;
                superBurst = false;
            }
        }

        if (GameManager.instance.blastOff == true)
        {
            vel.x = 0f;
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
