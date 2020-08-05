using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetitionPoints : UIPoints {
    public int num;

    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetRepetitions (num);
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}