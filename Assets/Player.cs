using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    Animator anim;
    public GameObject tpRoll;
    Transform player;
    public Text text;

    public LaneManager lm;
    public float playerspeed = 4;
    int hp = 2;
    bool tpActive = false;


    void Start()
    {
        player = transform;
        anim = GetComponent<Animator>();
        
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

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            tpActive = !tpActive;
            anim.SetBool("toiletpaper", tpActive);
            tpRoll.active = tpActive;
            Debug.Log("hehe");
        }
        
        player.position = new Vector3(Mathf.Lerp(player.position.x, 
            lm.GetActiveLane().x, 0.3f), 
            (player.position.y + playerspeed * Time.deltaTime) , -3);
    }

    int UpdateHp(int i)
    {
        hp += i;
        text.text = "HP: " + hp.ToString();
        return hp;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        UpdateHp(-1);
    }
}
