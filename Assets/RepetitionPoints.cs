using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetitionPoints : UIPoints {
    public int num;

    public override void SetPoints () {
        if (locked) return;

        Points points = dm.GetRepetitions (num);
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}