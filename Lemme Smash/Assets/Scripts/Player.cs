using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Becky Combo Buttons")]

    [SerializeField]
    private KeyCode blueKeyCode;

    [SerializeField]
    private KeyCode redKeyCode;

    [SerializeField]
    private KeyCode greenKeyCode;

    [SerializeField]
    private KeyCode yellowKeyCode;

    public int score;

    private int heatMultiplier;
    private HeatMeter heatMeter;

    private int beckyMultiplier;
    private Action beckyComboSuccessCallback;
    private BeckyColorPicker beckyColorPicker;
    private bool beckyComboAttempted;

    private const int BECKY_BONUS_MULTIPLIER = 2;
    private const float BECKY_BONUS_TIME = 7f;

    private const int BASE_SCORE_INCR = 10;
    private const int BASE_HEAT_INCR = 5;
    private const int BASE_HEAT_DECR = 20;

    enum HeatMultiplier
    {
        NORMAL = 1,
        HOT = 2,
        ON_FIRE = 4
    }

    public int HeatValue
    {
        get
        {
            return heatMeter.Value;
        }
    }

    private bool IsBeckyBonusActive
    {
        get
        {
            return beckyMultiplier > 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        heatMeter = GetComponentInChildren<HeatMeter>();
        heatMultiplier = (int) HeatMultiplier.NORMAL;

        beckyColorPicker = GameObject.FindGameObjectWithTag("Becky").GetComponent<BeckyColorPicker>();
        beckyMultiplier = 1;
        beckyComboAttempted = false;
        beckyComboSuccessCallback = () => {

            if (!IsBeckyBonusActive)
            {
                StartCoroutine(BeckyBonusMultiplier());
            }
        };

        // Set the callback
        InputBlock[] inputBlocks = GetComponentsInChildren<InputBlock>();
        foreach (var inputBlock in inputBlocks)
        {
            inputBlock.ValidHitCallback = () => {
                heatMeter.Value += BASE_HEAT_INCR;
                DetermineHeatMultiplier();
                AddScore(BASE_SCORE_INCR);

                //Debug.Log($"Heat = {heatMeter.Value}, Score = {score}");
            };

            inputBlock.MissCallback = () => {
                heatMeter.Value -= BASE_HEAT_DECR;
                DetermineHeatMultiplier();

                //Debug.Log($"Heat = {heatMeter.Value}, Score = {score}");
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetermineHeatMultiplier();

        if (!beckyColorPicker.IsThinking && beckyComboAttempted)
        {
            beckyComboAttempted = false;
        }

        // TODO: add X-Box controls
        if (Input.GetKeyDown(blueKeyCode))
        {
            AttemptBeckyCombo(BeckyColorPicker.BeckyColor.BLUE);
        }
        else if (Input.GetKeyDown(redKeyCode))
        {
            AttemptBeckyCombo(BeckyColorPicker.BeckyColor.RED);
        }
        else if (Input.GetKeyDown(greenKeyCode))
        {
            AttemptBeckyCombo(BeckyColorPicker.BeckyColor.GREEN);
        }
        else if (Input.GetKeyDown(yellowKeyCode))
        {
            AttemptBeckyCombo(BeckyColorPicker.BeckyColor.YELLOW);
        }
    }

    private void LateUpdate()
    {
        // Debug.Log($"Multiplier is now {multiplier}, which is {(Multiplier) multiplier}!");
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd * heatMultiplier * beckyMultiplier;
    }

    private void AttemptBeckyCombo(BeckyColorPicker.BeckyColor chosenColor)
    {
        if (beckyColorPicker.IsThinking && !beckyComboAttempted)
        {
            beckyColorPicker.SetColorPressed(chosenColor, beckyComboSuccessCallback);
            beckyComboAttempted = true;
        }
    }

    private void DetermineHeatMultiplier()
    {
        switch (heatMeter.State)
        {
            case HeatMeter.HeatMeterState.HOT:
                heatMultiplier = (int)HeatMultiplier.HOT;
                break;

            case HeatMeter.HeatMeterState.ON_FIRE:
                heatMultiplier = (int)HeatMultiplier.ON_FIRE;
                break;

            default:
                heatMultiplier = (int)HeatMultiplier.NORMAL;
                break;
        }
    }

    private IEnumerator BeckyBonusMultiplier()
    {
        beckyMultiplier = BECKY_BONUS_MULTIPLIER;
        yield return new WaitForSeconds(BECKY_BONUS_TIME);
        beckyMultiplier = 1;
    }
}
