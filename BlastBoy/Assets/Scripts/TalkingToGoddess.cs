using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingToGoddess : MonoBehaviour
{

    public Color defaultColor;
    public Color transparentColor;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        sprite.color = Color.Lerp(defaultColor, transparentColor, Mathf.PingPong(Time.time, 1));

        if (GameManager.instance.sceneID == 6 && GameManager.instance.blastOff)
         {
            Destroy(gameObject);
         }
    }
}
