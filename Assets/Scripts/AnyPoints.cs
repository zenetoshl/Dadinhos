using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyPoints : UIPoints
{
    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetAny ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
