using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{

    public GameObject player;

    public GameObject floor;

    bool spawned;

    float randomNum;

    bool obstacleSpawned;

    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            if (!spawned)
            {
                Instantiate(floor, new Vector3(transform.position.x + 16f, transform.position.y, transform.position.z), Quaternion.identity);
                spawned = true;
            }
        }

        if (randomNum >= .6f && transform.position.x != 0f)
        {
            if (!obstacleSpawned)
            {
                Instantiate(obstacle, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                obstacleSpawned = true;
            }
        }
    }
}
