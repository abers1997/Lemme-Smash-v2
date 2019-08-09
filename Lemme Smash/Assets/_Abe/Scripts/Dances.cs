using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dances : MonoBehaviour
{
    // Start is called before the first frame update

    public HeatMeter hm;
    public Animator anim;

    public void Update()
    {

        switch (hm.State)
        {
            case HeatMeter.HeatMeterState.HOT:
                Debug.Log("Player 1 is Hot");
                anim.SetInteger("isHot", 1);
                anim.SetInteger("isOnFire", 0);
                break;

            case HeatMeter.HeatMeterState.ON_FIRE:
                Debug.Log("Player 1 is On Fire!");
                anim.SetInteger("isOnFire", 1);
                anim.SetInteger("isHot", 0);
                break;

            default:
                Debug.Log("Player 1 is Normal");
                anim.SetInteger("isHot", 0);
                anim.SetInteger("isOnFire", 0);
                break;
        }


        

        
    }


}
