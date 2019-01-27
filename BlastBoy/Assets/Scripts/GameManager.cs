using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int sceneID;

    public bool blastOff;
    float blastOffTimer;

    public bool discoverJump;
    public bool discoverBurst;

    public Text playerText;
    public Text goddessText;

    AudioSource audio;

    public AudioClip blastOffSound;

    public bool inactive;

    int dialogueCount;

   public bool tornadoActive;
    bool tornadoSpawned;

    public GameObject Tornado;

    public bool reloadScene;

    public Color color1;
    public Color color2;

    public bool goddessReveal;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        instance = this;

        audio = GetComponent<AudioSource>();
        dialogueCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        //Dialogue
        if (discoverJump)
        {
            playerText.text = "?";
        }
        else if (!discoverJump)
        {
            playerText.text = "";
        }

        if (sceneID == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                dialogueCount += 1;
            }

            switch (dialogueCount)
            {
                case 0:
                    inactive = true;
                    break;
                case 1:
                    playerText.text = "Hello?";
                    break;
                case 2:
                    playerText.text = "Can you tell me where I am?";
                    break;
                case 3:
                    playerText.text = "Ummmm.... Hello?";
                    break;
                case 4:
                    inactive = false;
                    break;
            }
        }

        if (sceneID == 4)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                dialogueCount += 1;
            }

            switch (dialogueCount)
            {
                case 0:
                    inactive = true;
                    break;
                case 1:
                    playerText.text = "Where am I now?";
                    break;
                case 2:
                    playerText.text = "I can't see anything.";
                    break;
                case 3:
                    playerText.text = "Wait, what's that noise?";
                    if (!tornadoSpawned)
                    {
                        Instantiate(Tornado, new Vector3(-5f, 2.5f, 0f), Quaternion.Euler(0,0,190));
                        tornadoSpawned = true;
                    }
                    break;
                case 4:
                    playerText.text = "Oh no...";
                    break;
                case 5:
                    playerText.text = "";
                    inactive = false;
                    tornadoActive = true;
                    break;
            }
        }

        if (sceneID == 6)
        {

            goddessText.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 2));

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                dialogueCount += 1;
            }

            switch (dialogueCount)
            {
                case 0:
                    playerText.text = "";
                    goddessText.text = "";
                    break;
                case 1:
                    playerText.text = "Where am I now?";
                    break;
                case 2:
                    playerText.text = "What are these things around me?";
                    break;
                case 3:
                    playerText.text = "They feel very... calming.";
                    break;
                case 4:
                    playerText.text = "Like home...";
                    break;
                case 5:
                    goddessText.text = "Home?";
                    break;
                case 6:
                    goddessText.text = "What is home?";
                    break;
                case 7:
                    goddessText.text = "";
                    playerText.text = "Who said that?";
                    break;
                case 8:
                    playerText.text = "";
                    goddessReveal = true;
                    break;
                case 9:
                    playerText.text = "WHOA!";
                    break;
                case 10:
                    playerText.text = "";
                    goddessText.text = "What is home?";
                    break;
                case 11:
                    playerText.text = "Uhh, home is...";
                    goddessText.text = "";
                    break;
                case 12:
                    playerText.text = "Home is where I want to go after an adventure.";
                    goddessText.text = "";
                    break;
                case 13:
                    playerText.text = "";
                    goddessText.text = "Did you have an adventure?";
                    break;
                case 14:
                    playerText.text = "Yeah";
                    goddessText.text = "";
                    break;
                case 15:
                    playerText.text = "";
                    goddessText.text = "How was it?";
                    break;
                case 16:
                    playerText.text = "Well, I left myself on one planet.";
                    goddessText.text = "";
                    break;
                case 17:
                    playerText.text = "That felt weird...";
                    goddessText.text = "";
                    break;
                case 18:
                    playerText.text = "And I was chased by a dimensional tornado.";
                    goddessText.text = "";
                    break;
                case 19:
                    playerText.text = "";
                    goddessText.text = "That doesn't sound very calming.";
                    break;
                case 20:
                    playerText.text = "It wasn't";
                    goddessText.text = "";
                    break;
                case 21:
                    playerText.text = "";
                    goddessText.text = "Do you want to go home?";
                    break;
                case 22:
                    playerText.text = "Yeah...";
                    goddessText.text = "";
                    break;
                case 23:
                    playerText.text = "I don't know how though...";
                    goddessText.text = "";
                    break;
                case 24:
                    playerText.text = "";
                    goddessText.text = "Well, I can send you where you will feel most calm.";
                    break;
                case 25:
                    playerText.text = "Is that home?";
                    goddessText.text = "";
                    break;
                case 26:
                    playerText.text = "";
                    goddessText.text = "That's up to you.";
                    break;
                case 27:
                    playerText.text = "";
                    goddessText.text = "Are you ready?";
                    break;
                case 28:
                    playerText.text = "I guess.";
                    goddessText.text = "";
                    break;
                case 29:
                    playerText.text = "It was nice talking with you strange space goddess.";
                    goddessText.text = "";
                    break;
                case 30:
                    playerText.text = "Good way to end an adventure.";
                    goddessText.text = "";
                    break;
                case 31:
                    playerText.text = "";
                    goddessText.text = "Ditto kiddo";
                    break;
                case 32:
                    playerText.text = "";
                    goddessText.text = "";
                    blastOff = true;
                    break;
            }
        }

        if (sceneID == 8)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player.transform.position.x <= -6f)
            {
                blastOffTimer += 1 * Time.deltaTime;
                if (blastOffTimer >= 3f)
                {
                    SceneManager.LoadScene(sceneID += 1);
                }
            }
        }

        if (sceneID == 1 || sceneID == 3 || sceneID == 5 || sceneID == 7)
        {
            blastOff = true;
        }

        if (blastOff)
        {
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(blastOffSound, 1f);
            }

            blastOffTimer += 1 * Time.deltaTime;

            if (blastOffTimer >= 3f)
            {
                SceneManager.LoadScene(sceneID += 1);
                blastOff = false;
            }
        }

        if (reloadScene)
        {
            SceneManager.LoadScene(sceneID);
        }

    }

}
