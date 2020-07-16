using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public abstract class UIPoints : MonoBehaviour
{
    public TextMeshProUGUI PointsText;
    public DiceManager dm;
    public bool locked = false;
    public int value = 0;

    private void Start () {
        RollManager.instance.OnRoll += SetPoints;
    }

    public abstract void SetPoints();

    public void HighlightDices (List<int> indexes) {
        //highlight dos dados
        return;
    }

    public void Lock () {
        if (locked) return;

        locked = true;
        PointsText.color = new Color32 (0, 0, 0, 255);
        LockManager.selected = null;
    }

    public void Select () {
        if (locked) return;

        PointsText.color = new Color32 (0, 0, 0, 255);
        LockManager.selected = this;
        Debug.Log("selected");
    }
}
