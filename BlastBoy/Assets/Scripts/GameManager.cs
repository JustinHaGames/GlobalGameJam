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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        instance = this;

        audio = GetComponent<AudioSource>();
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

        if (sceneID == 1 || sceneID == 3)
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

    }

}
