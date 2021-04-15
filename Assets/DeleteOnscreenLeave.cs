using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnscreenLeave : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < player.transform.position.y - 160)
        {
            Destroy(gameObject);
        }
    }
}
