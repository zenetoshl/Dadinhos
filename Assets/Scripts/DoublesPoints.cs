using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublesPoints : UIPoints
{
    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetDoublePair ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
