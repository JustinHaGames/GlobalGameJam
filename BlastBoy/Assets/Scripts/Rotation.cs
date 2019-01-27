using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public float rotationIntensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.sceneID != 7)
        {
            transform.Rotate(new Vector3(0, 0, -5f), Time.deltaTime * rotationIntensity);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 5f), Time.deltaTime * rotationIntensity);
        }
    }
}
