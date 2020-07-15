using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberPoints : MonoBehaviour {
    // Start is called before the first frame update
    public TextMeshProUGUI PointsText;
    public DiceManager dm;
    public int num;
    public bool locked = false;

    public void SetPoints () {
        if (locked) return;

        Points points = dm.GetNumber (num);
        PointsText.text = "" + points.points;
        HighlightDices (points.indexes);
    }

    public void HighlightDices (List<int> indexes) {
        //highlight dos dados
        return;
    }

    public void Lock () {
        if (locked) return;

        locked = true;
        PointsText.color = new Color32 (0, 0, 0, 255);
    }

    public void Select(){
        if (locked) return;

        PointsText.color = new Color32 (0, 0, 0, 255);
        //enviar this para o lockmanager
    }

}