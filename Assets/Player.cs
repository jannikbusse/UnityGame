using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    int tpLeft = 200;
    Animator anim;
    public GameObject tpRoll;
    public GameObject trailPrefab;
    public GameObject endPrefab;


    Transform player;
    public Text text;

    public LaneManager lm;
    public Toiletpaper activePaper;
    public float playerspeed = 4;
    int hp = 2;
    bool tpActive = false;

    bool isInlockdown = false;

    float toiletrun_duration = 1.5f;
    float toiletrun_end = 0;


    void Start()
    {
        player = transform;
        anim = GetComponent<Animator>();
        
    }

    public void TryMove(int dir)
    {
        if(tpActive)
            return;
        
        if (dir < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 3, 
                Vector2.left, 20,1 << LayerMask.NameToLayer("border")); 
            if(hit.collider == null)
                lm.ChangeLane(dir);
          
            
        }

        else
        {   
             RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 3, 
                Vector2.right, 20,1 << LayerMask.NameToLayer("border")); 
            if(hit.collider == null)
                lm.ChangeLane(dir);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            TryMove(-1);
        }
        else if (Input.GetKeyDown("d"))
        {
            TryMove(1);           
        }



        else if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }



        if(tpActive && Time.time > toiletrun_end)
            LeaveToiletRun();
        
        player.position = new Vector3(Mathf.Lerp(player.position.x, 
            lm.GetActiveLane().x, 0.3f), 
            (player.position.y + playerspeed * Time.deltaTime) , -3);

        if(isInlockdown && !tpActive){
            lm.damage += Time.deltaTime * 48;
        }
    }

    public void TryJump()
    {
        if(IsInTransition())
                return;
            if(!tpActive && tpLeft > 0)
            {
                UpdateHp(-1);
                EnterToiletRun();
            }

    }

    

    bool IsInTransition()
    {
        float dst = Mathf.Abs(lm.GetActiveLane().x - transform.position.x);
        if(dst < 1f)
        {
            transform.position = new Vector3(lm.GetActiveLane().x, transform.position.y, transform.position.z);
            return false;
        }
        return true;
    }

    void DetachTrail()
    {
        activePaper.isConnected = false;
        GameObject end = Instantiate(endPrefab, new Vector3(transform.position.x, transform.position.y - 8f, transform.position.z), Quaternion.identity);
        end.transform.parent = activePaper.gameObject.transform;

    }
    void SpawnTpTrail()
    {
        GameObject trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        activePaper = trail.GetComponent<Toiletpaper>();

    }

    void EnterToiletRun()
    {
        if(IsInTransition() || tpActive)
            return;
        lm.damage -= 13;
        toiletrun_end = Time.time + toiletrun_duration;
        tpActive = true;
        anim.SetBool("toiletpaper", tpActive);
        tpRoll.SetActive(tpActive);
        
        SpawnTpTrail();
        
    }

     void LeaveToiletRun()
    {
        if(!tpActive)
            return;
        tpActive = false;
        anim.SetBool("toiletpaper", tpActive);
        tpRoll.SetActive(tpActive);        
        DetachTrail();
        
    }

    int UpdateHp(int i)
    {
        tpLeft += i;
        text.text = "HP: " + tpLeft.ToString();
        return hp;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        

        if(other.gameObject.tag == "lockdown")
        {
            isInlockdown = false;
            Debug.Log(lm.damage);
            LeaveToiletRun();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "virus")
        {
            Destroy(other.gameObject);
            if(!tpActive)
                lm.damage += 25;
        }

        if(other.gameObject.tag == "boost")
        {
            lm.damage -= 25;
            Debug.Log("Boost");
            Destroy(other.gameObject);
        }
        

        if(other.gameObject.tag == "lockdown")
        {
            //EnterToiletRun();
            isInlockdown = true;
        }

        if(other.gameObject.tag == "tpboost")
        {
            UpdateHp(1);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "gameover")
        {
            lm.damage = 0;
        }


    }
}
