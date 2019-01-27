using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public Text creditText;

    int buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            buttonPressed += 1;
        }

        if (buttonPressed >= 1f)
        {
            creditText.text = "Thank You for Playing!";
        }
    }
}
