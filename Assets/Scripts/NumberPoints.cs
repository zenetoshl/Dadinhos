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
}