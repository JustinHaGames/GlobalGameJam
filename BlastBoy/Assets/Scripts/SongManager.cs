using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.sceneID == 2)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        if (GameManager.instance.sceneID == 4 && GameManager.instance.tornadoActive)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        if (GameManager.instance.sceneID == 6 && GameManager.instance.goddessReveal)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
}
