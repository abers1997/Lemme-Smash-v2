using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeckyStates : MonoBehaviour
{
    private Player[] players;

    private Player favoredPlayer;
    private BeckyState beckyState;

    public enum BeckyState
    {
        IDLE,
        AMAZED,
        IN_LOVE,
        HINTING
    }

    void Start()
    {
        favoredPlayer = null;

        // Find all the player objects
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        players = new Player[playerObjs.Length];
        for (int i = 0; i < playerObjs.Length; ++i)
        {
            players[i] = playerObjs[i].GetComponent<Player>();
        }
    }

    void Update()
    {
        SetFavoredPlayer();
        SetState();
    }

    // NOTE: this logic doesn't handle ties well
    private void SetFavoredPlayer()
    {
        int maxHeat = 0;
        foreach (var player in players)
        {
            int playerHeatValue = player.HeatValue;
            if (maxHeat < playerHeatValue)
            {
                maxHeat = playerHeatValue;
                favoredPlayer = player;
            }
        }
    }

    // TODO: change the hardcoded numbers.
    private void SetState()
    {
        // TODO: replace the color-changing with animation-setting
        if (!(favoredPlayer is null))
        {
            int favoredPlayerHeat = favoredPlayer.HeatValue;
            if (favoredPlayerHeat >= 0f && favoredPlayerHeat < 33f)
            {
                beckyState = BeckyState.IDLE;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 250f, 250f, 255f);
            }
            if (favoredPlayerHeat >= 34f && favoredPlayerHeat < 66f)
            {
                beckyState = BeckyState.AMAZED;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0f, 255f);
            }
            if (favoredPlayerHeat >= 67f && favoredPlayerHeat <= 100f)
            {
                beckyState = BeckyState.IN_LOVE;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);
            }
        }
        else
        {
            beckyState = BeckyState.IDLE;
        }
    }
}
