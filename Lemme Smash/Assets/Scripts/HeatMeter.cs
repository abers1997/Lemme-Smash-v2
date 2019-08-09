using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMeter : MonoBehaviour
{

    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameObject foreground;

    private int value;
    
    private const int MAX_VALUE = 100;
    private const float HOT_PCNT = 0.33f;
    private const float FIRE_PCNT = 0.66f;

    // Decrementing things
    private const float DECR_TIME = 2f;
    private const int DECR_VAL = 1;

    public enum HeatMeterState
    {
        NORMAL,
        HOT,
        ON_FIRE
    }

    public HeatMeterState State
    {
        get; private set;
    }

    public int Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;

            if (this.value > MAX_VALUE)
            {
                this.value = MAX_VALUE;
            }
            else if (this.value < 0)
            {
                this.value = 0;
            }

            foreground.transform.localScale = new Vector3((1 - (float)this.value / MAX_VALUE), 1);


            
            if (this.value >= FireValue)
            {
                State = HeatMeterState.ON_FIRE;
            }
            else if (this.value >= HotValue)
            {
                State = HeatMeterState.HOT;
            }
            else
            {
                State = HeatMeterState.NORMAL;
            }
        }
    }

    private int FireValue
    {
        get
        {
            return (int)(FIRE_PCNT * MAX_VALUE);
        }
    }

    private int HotValue
    {
        get
        {
            return (int)(HOT_PCNT * MAX_VALUE);
        }
    }

    private void Awake()
    {
        Value = 0;
        foreground.transform.position = background.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Decrement());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Value = {Value}, bird is now: {State}");
    }

    // Incrementally decrements heat meter independent of all else
    private IEnumerator Decrement()
    {
        while (true)
        {
            yield return new WaitForSeconds(DECR_TIME);

            if (Value > 0)
            {
                Value -= DECR_VAL;
            }
        }
    }
}
