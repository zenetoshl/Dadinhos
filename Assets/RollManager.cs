using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollManager : MonoBehaviour {
    public TextMeshProUGUI text;

    public static RollManager instance;
    public event Action OnRoll;

    public const int maxRollCount = 3;
    public int rollCount = 0;

    private void Awake () {
        instance = this;
    }
    private void Start () {
        LockManager.instance.OnNewTurn += NewTurn;
        UpdateUI ();
    }

    public void Roll () {
        if (!DiceManager.rolling && maxRollCount <= rollCount) return;
        DiceManager.ChangeRollStatus ();
        if (!DiceManager.rolling) {
            if (OnRoll != null) {
                OnRoll ();
            }
        } else {
            rollCount++;
        }
        UpdateUI ();
    }

    public void UpdateUI () {
        if (DiceManager.rolling) {
            text.text = "Stop Roll";
        } else {
            text.text = "Roll X" + (maxRollCount - rollCount);
        }
    }

    public void NewTurn () {
        rollCount = 0;
        UpdateUI();
    }
}