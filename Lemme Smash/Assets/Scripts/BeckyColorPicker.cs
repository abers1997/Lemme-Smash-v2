using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeckyColorPicker : MonoBehaviour
{
    private float waitingTime;
    private float thinkingTime;
    private bool isPressed;
    private BeckyColor currColor;

    public bool IsThinking
    {
        get; private set;
    }

    public enum BeckyColor
    {
        RED,
        BLUE,
        GREEN,
        YELLOW
    }

    // Start is called before the first frame update
    private void Awake()
    {
        waitingTime = 2f;
        thinkingTime = 1f;
        IsThinking = false;
        isPressed = false;
        currColor = 0; // Sets it to whatever the first value of the enum is

        StartCoroutine(ChooseColor());
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SetColorPressed(BeckyColor colorPressed, Action successCallback = null)
    {
        if (IsThinking)
        {
            if (!isPressed)
            {
                if (colorPressed == currColor)
                {
                    isPressed = true;

                    if (!(successCallback is null))
                    {
                        successCallback();
                    }
                }
            }

            // else call failCallback (?)
        }
    }

    private IEnumerator ChooseColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingTime);

            // Choose a random color

            // This hideous line gets the maximum numeric value of the BeckyColor enum.
            int maxColorValue = (int)Enum.GetValues(typeof(BeckyColor)).Cast<BeckyColor>().Max();

            // Then choose a value from the enum values from Random as the current color.
            currColor = (BeckyColor)UnityEngine.Random.Range(0, maxColorValue);

            Debug.Log($"Becky is thinking of {currColor}!");

            IsThinking = true;

            // Wait for thinking time; stop prematurely if a player chooses the correct color.
            for (float timer = thinkingTime; timer >= 0; timer -= Time.deltaTime * Time.timeScale)
            {
                if (isPressed)
                {
                    break;
                }
                yield return null;
            }

            Debug.Log("Time's up!");

            IsThinking = false;
            isPressed = false;
        }
    }
}
