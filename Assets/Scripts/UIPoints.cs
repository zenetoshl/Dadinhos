using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIPoints : MonoBehaviour {
    public TextMeshProUGUI PointsText;
    public int playerId;
    public bool locked = false;
    public int value = 0;

    private void Start () {
        RollManager.instance.OnRoll += SetPoints;
        LockManager.instance.OnNewTurn += ResetValue;
    }

    public abstract void SetPoints (int id);

    public void HighlightDices (List<int> indexes) {
        //highlight dos dados
        return;
    }

    public void Lock () {
        if (locked || DiceManager.reseted || RollManager.playerId != playerId) return;
        TotalPointsManager.instance.totalPoints[playerId - 1].Lock ();
        locked = true;
        PointsText.color = new Color32 (255, 255, 255, 255);
        LockManager.selected = null;
    }

    public void Select () {
        if (locked || DiceManager.reseted || RollManager.playerId != playerId) return;
        TotalPointsManager.instance.totalPoints[playerId - 1].selectedPoints = value;
        TotalPointsManager.instance.totalPoints[playerId - 1].UpdateUI ();
        PointsText.color = new Color32 (200, 200, 200, 255);
        LockManager.Select (this);

    }

    public void Unselect () {
        TotalPointsManager.instance.totalPoints[playerId - 1].UpdateUI ();
        PointsText.color = new Color32 (220, 220, 220, 255);
    }

    public void ResetValue () {
        if (locked) return;

        value = 0;
        PointsText.text = "" + value;
    }
}