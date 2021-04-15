using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toiletpaper : MonoBehaviour
{

     SpriteRenderer trail;
    public bool isConnected = true;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trail = gameObject.GetComponent<SpriteRenderer>();
        transform.position = player.transform.position;
        trail.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(isConnected)
            trail.size = new Vector2(16,trail.transform.position.y - player.transform.position.y + 8);
        else if(Mathf.Abs(trail.transform.position.y - trail.size.y - player.transform.position.y) > 160)
            Destroy(gameObject);
    }




}
