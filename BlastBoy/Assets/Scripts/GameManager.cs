using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int sceneID;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
