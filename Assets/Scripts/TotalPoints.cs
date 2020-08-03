using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalPoints : MonoBehaviour
{
    public TextMeshProUGUI PointsText;
    public DiceManager dm;
    public bool locked = false;
    public List<UIPoints> uiList;

    private void Start () {
        RollManager.instance.OnRoll += SetPoints;
    }

    public void SetPoints () {
        if (locked) return;
        int total = 0;
        foreach (UIPoints uiPoints in uiList)
        {
            if(uiPoints.locked){
                total += uiPoints.value;
            }
        }
        PointsText.text = "" + total;
    }
}
