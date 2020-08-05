using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberPoints : UIPoints {
    public int num;

    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetNumber (num);
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }

    public override void Lock(){
        if (locked || DiceManager.reseted || RollManager.playerId != playerId) return;
        TotalPointsManager.instance.totalPoints[playerId - 1].Lock ();
        locked = true;
        PointsText.color = new Color32 (255, 255, 255, 255);
        LockManager.selected = null;
        BonusManager.instance.AddPointsToSum(value);
    }
}