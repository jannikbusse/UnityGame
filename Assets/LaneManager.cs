using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{

    float lastSpawn = 0;
    float spawnCD = 0.5f;

    public GameObject virusPrefab;
    public GameObject lockdownPrefab;
    public GameObject borderPrefab;
    public GameObject boostPrefab;
    public GameObject tpboostprefab;


    public Player player;

    List<lane> lanes;
    List<float> laneLocks;
    public float width = 6;
    public float damage = 0;
    public int activeLane = 2;

    public struct lane
    {
        public float x;
        public float size;
    };

    public int laneNumber = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        lanes = new List<lane>();
        laneLocks = new List<float>();
        for(int i = 0; i < laneNumber; i ++)
        {
            lane l;
            l.x = -width/2 + (float)i * width/((float)(laneNumber-1));
            l.size = width / ((float)laneNumber-1);
            lanes.Add(l);
            laneLocks.Add(0);
        }
        Debug.Log(lanes[0].size);
    }

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(0, 1, 1.0f);
        foreach(lane l in lanes)
        {
            Debug.DrawLine(new Vector3(l.x - l.size/2f,-16,0), new Vector3(l.x- l.size/2f, 10000, 0), color);
            Debug.DrawLine(new Vector3(l.x + l.size/2f,-16,0), new Vector3(l.x + l.size/2f, 10000, 0), color);

        }

        if(Time.time > lastSpawn + spawnCD){
            SpawnObjective();
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


    public void SpawnVirus(int lane)
    {
        Instantiate(virusPrefab, new Vector3(lanes[lane].x, player.transform.position.y + 200, 0), Quaternion.identity);

    }

    public void SpawnLockDown(int lane)
    {
        Instantiate(lockdownPrefab, new Vector3(lanes[lane].x, player.transform.position.y + 200, 0), Quaternion.identity);
        laneLocks[lane] = Time.time + 1;

    }

    public void SpawnBorder()
    {
        int lane = Random.Range(1,laneNumber - 1 );
        int side = Random.Range(0,2);
        int length = Random.Range(16,200);

        GameObject go;
        if(side == 0)
        {
            go = Instantiate(borderPrefab, new Vector3(lanes[lane].x +  lanes[lane].size/2f, player.transform.position.y + 200, 0), Quaternion.identity);

        }else
            go =Instantiate(borderPrefab, new Vector3(lanes[lane].x  -  lanes[lane].size/2f, player.transform.position.y + 200, 0), Quaternion.identity);

        go.GetComponent<SpriteRenderer>().size = new Vector2(4f, length);
        go.GetComponent<BoxCollider2D>().size = new Vector2(4f,length);

        


    }

    void SpawnBoost(int lane){
        Instantiate(boostPrefab, new Vector3(lanes[lane].x, player.transform.position.y + 200, 0), Quaternion.identity);
    }

    void SpawnTpBoost(int lane){
        Instantiate(tpboostprefab, new Vector3(lanes[lane].x, player.transform.position.y + 200, 0), Quaternion.identity);
    }
    public void SpawnObjective()
    {
        int lane = Random.Range(0,laneNumber );
        if(laneLocks[lane] > Time.time)
        {
            return;
        }
        lastSpawn = Time.time;

        float r = Random.Range(0,100);
        if(r < 10){
            SpawnLockDown(lane);
        }
        else if(r < 20){
            SpawnBorder();
        }
        else if(r < 23){
            SpawnBoost(lane);
        }
        else if(r < 26){
            SpawnTpBoost(lane);
        }
        else{       
            SpawnVirus(lane);
        }


    }
}
