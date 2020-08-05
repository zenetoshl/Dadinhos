using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusManager : MonoBehaviour {
    
    public TextMeshProUGUI pointsText;
    public int playerId;
    public bool locked = false;
    private int value = 0;
    private int pointsSum = 0;
    public int minPoints = 40;
    public int bonusValue = 20;

    public static BonusManager instance;
    private void Awake () {
        instance = this;
    }

    private void Start() {
        UpdateUi();
    }
    
    private void UpdateUi(){
        pointsText.text = "" + value; 
    }

    public void AddPointsToSum(int points){
        if(locked) return;
        pointsSum += points;
        if( pointsSum >= minPoints){
            value = bonusValue;
            TotalPointsManager.instance.totalPoints[playerId -1].AddBonus(value);
            locked = true;
        }
        UpdateUi();
    }
}