using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{

    List<lane> lanes;
    public float x = 0;
    public float width = 4;
    public int activeLane = 2;

    public struct lane
    {
        public float x;
        public float size;
    };

    int laneNumber = 5;
    



    // Start is called before the first frame update
    void Start()
    {
        lanes = new List<lane>();
        for(int i = 0; i < laneNumber; i ++)
        {
            lane l;
            l.x = -2 + (float)i * width/(laneNumber-1);
            l.size = width / laneNumber;
            lanes.Add(l);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(0, 1, 1.0f);
        foreach(lane l in lanes)
        {
            Debug.DrawLine(new Vector3(l.x,-6,0), new Vector3(l.x, 10, 0), color);
        }
       
    }

    public lane GetActiveLane()
    {
        return lanes[activeLane];
    }

    public void ChangeLane(int i)
    {
        activeLane = Mathf.Max(Mathf.Min(activeLane + i, laneNumber - 1), 0);
    }
}
