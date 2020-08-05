using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollManager : MonoBehaviour {
    public TextMeshProUGUI rollText;
    public TextMeshProUGUI playerName;

    public static RollManager instance;
    public event Action<int> OnRoll;

    public const int maxRollCount = 3;
    public int rollCount = 0;

    public static int playerId;

    public const int nRounds = 13;
    public int round;

    public string[] playerNames = { "player_1", "Player_2" };

    private void Awake () {
        instance = this;
        LockManager.instance.OnNewTurn += NewTurn;
    }
    private void Start () {
        playerName.text = playerNames[0];
        round = 1;
        playerId = 1;
        UpdateUI ();
    }

    public void Roll () {
        if (!DiceManager.rolling && maxRollCount <= rollCount) return;
        DiceManager.ChangeRollStatus ();
        if (!DiceManager.rolling) {
            if (OnRoll != null) {
                OnRoll (playerId);
            }
        } else {
            rollCount++;
        }
        UpdateUI ();
    }

    public void UpdateUI () {
        if (DiceManager.rolling) {
            rollText.text = "Stop Roll";
        } else {
            rollText.text = "Roll X" + (maxRollCount - rollCount);
        }
    }

    public void NewTurn () {
        int nPlayers = DiceManager.instance.nPlayers;
        rollCount = 0;
        if (playerId == nPlayers) {
            round++;
            //new round animation
            if (round > nRounds) {
                //endgame
            }
        }
        playerId = 1 + (playerId % nPlayers);
        playerName.text = playerNames[playerId - 1];
        UpdateUI ();
    }
}