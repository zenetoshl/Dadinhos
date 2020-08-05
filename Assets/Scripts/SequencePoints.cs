using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePoints : UIPoints
{
    public int size;

    public override void SetPoints (int id) {
        if (locked || id != playerId) return;

        Points points = DiceManager.instance.GetSequences (size);
        value = points.points;
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }
}
