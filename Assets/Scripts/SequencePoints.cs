using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePoints : UIPoints
{
    public int size;

    public override void SetPoints () {
        if (locked) return;

        Points points = dm.GetSequences (size);
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
