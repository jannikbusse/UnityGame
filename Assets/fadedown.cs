using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class fadedown : MonoBehaviour
{

    public LaneManager lm;
    public Vector3 target = new Vector3(0, -80, 10);
    public Vector3 criticalTarget = new Vector3(0, -60, 10);



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(lm.damage >= 25)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, criticalTarget, 0.1f);
        }
        else{
            transform.localPosition = Vector3.Slerp(transform.localPosition, target, 0.1f);
        }
    }
}
