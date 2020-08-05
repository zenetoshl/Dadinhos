using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalPointsManager : MonoBehaviour {
    public TotalPoints[] totalPoints;
    public static TotalPointsManager instance;
    private void Awake () {
        instance = this;
    }
}