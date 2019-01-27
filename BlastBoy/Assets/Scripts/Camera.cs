using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.sceneID == 0)
        {

            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

            if (transform.position.x <= 0f)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }

            if (transform.position.x >= 30f)
            {
                transform.position = new Vector3(30, transform.position.y, transform.position.z);
            }


        }
    }
}
