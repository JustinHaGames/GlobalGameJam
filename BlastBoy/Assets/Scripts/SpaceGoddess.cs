using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGoddess : MonoBehaviour
{

    SpriteRenderer sprite;

    public Color defaultColor;
    public Color transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.instance.goddessReveal)
        {
            sprite.color = transparentColor;
        }
        else
        {
            sprite.color = Color.Lerp(transparentColor, defaultColor, Mathf.Lerp(0,1, Time.time * .15f));
        }

    }

}
