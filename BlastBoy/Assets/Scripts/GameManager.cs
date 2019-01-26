using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int sceneID;

    public bool blastOff;
    float blastOffTimer;

    AudioSource audio;

    public AudioClip blastOffSound;

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
