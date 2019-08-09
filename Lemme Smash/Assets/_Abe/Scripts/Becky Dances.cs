using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeckyDances : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject becky;

    public Player p1_script;
    public Player p2_script;

    public void Start()
    {

        p1_script = p1.GetComponent<Player>();
        p2_script = p1.GetComponent<Player>();

        

    }

    public void Update()
    {
        if (p2_script.score > p1_script.score)
        {

        }
    }
}
