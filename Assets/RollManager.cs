using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateUI(){
        if(DiceManager.rolling){
            text.text = "Stop Roll";
        } else {
            text.text = "Roll";
        }
    }
}
