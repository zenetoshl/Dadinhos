using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyPoints : UIPoints
{
    public override void SetPoints () {
        if (locked) return;

        Points points = dm.GetAny ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
