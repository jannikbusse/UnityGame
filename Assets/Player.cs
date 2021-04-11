using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Transform player;

    public LaneManager lm;

    void Start()
    {
        player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            lm.ChangeLane(-1);
        }
        else if (Input.GetKeyDown("d"))
        {
            lm.ChangeLane(1);
        }

        player.position = new Vector3(lm.GetActiveLane().x, player.position.y, 0);
    }
}
