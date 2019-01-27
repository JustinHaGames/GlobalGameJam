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

    AudioSource audio;

    public AudioClip blastOffSound;

    public bool inactive;

    int dialogueCount;

   public bool tornadoActive;
    bool tornadoSpawned;

    public GameObject Tornado;

    public bool reloadScene;

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

        if (sceneID == 1 || sceneID == 3 || sceneID == 5)
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
