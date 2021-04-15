using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject toFollow;
    public GameObject bg1;
    public GameObject bg2;


    public LaneManager laneManager;
    public float offset;

    Material mat;
    float currentscroll = 0;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(toFollow.transform.position.y - transform.position.y+ 80 + laneManager.damage) < 0.9f)
            transform.position = new Vector3(transform.position.x, toFollow.transform.position.y  + 40 + laneManager.damage, transform.position.z);
        else{
            transform.position = Vector3.Lerp(transform.position, 
            new Vector3(transform.position.x, toFollow.transform.position.y  + 80+ laneManager.damage, transform.position.z), 0.043f);
        }

        if(Mathf.Abs(bg1.transform.position.y - transform.position.y) > 200)
        {
            bg1.transform.position = new Vector3(bg1.transform.position.x, bg2.transform.position.y + 192, bg1.transform.position.z);
        }
        if(Mathf.Abs(bg2.transform.position.y - transform.position.y) > 200)
        {
            bg2.transform.position = new Vector3(bg2.transform.position.x, bg1.transform.position.y + 192, bg2.transform.position.z);
        }

         

    }
}
