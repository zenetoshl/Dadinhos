using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublesPoints : UIPoints
{
    public override void SetPoints () {
        if (locked) return;

        Points points = dm.GetDoublePair ();
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
