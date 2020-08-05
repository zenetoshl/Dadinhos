using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHousePoints : UIPoints
{
    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetFullHouse ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
