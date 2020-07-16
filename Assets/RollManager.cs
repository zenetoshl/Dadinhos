using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollManager : MonoBehaviour {
    public TextMeshProUGUI text;

    public static RollManager instance;
    public event Action OnRoll;

    private void Awake () {
        instance = this;
    }
    private void Start () {
        UpdateUI ();
    }

    public void Roll () {
        DiceManager.ChangeRollStatus ();
        if (OnRoll != null && !DiceManager.rolling) {
            OnRoll ();
            Debug.Log("ola");
        }
        UpdateUI ();
    }

    public void UpdateUI () {
        if (DiceManager.rolling) {
            text.text = "Stop Roll";
        } else {
            text.text = "Roll";
        }
    }
}