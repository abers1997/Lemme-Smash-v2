using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public Timer timer;

    public Text resultText;

    private bool win;
    private bool lose;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer.timeRemaining <= 0 && win)
        {
            resultText.text = ("SMASH");
        }
        if (timer.timeRemaining <= 0 && lose)
        {
            resultText.text = ("PASS");
        }
    }
}
