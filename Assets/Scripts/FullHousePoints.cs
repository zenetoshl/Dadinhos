using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHousePoints : UIPoints
{
    public override void SetPoints () {
        if (locked) return;

        Points points = dm.GetFullHouse ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
