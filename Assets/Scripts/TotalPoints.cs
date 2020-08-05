using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalPoints : MonoBehaviour
{
    public TextMeshProUGUI PointsText;
    private int lockedPoints;
    public int selectedPoints;

    public int playerId;

    private void Start() {
        lockedPoints = 0;
        selectedPoints = 0;
    }

    public void UpdateUI () {
        int total = lockedPoints + selectedPoints;
        PointsText.text = "" + total;
    }

    public void Lock(){
        lockedPoints += selectedPoints;
        selectedPoints = 0;
    }

    public int GetPoints(){
        return lockedPoints;
    }

}
