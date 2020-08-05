using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TotalPointsManager : MonoBehaviour {
    public TotalPoints[] totalPoints;
    public static TotalPointsManager instance;
    public TextMeshProUGUI winnerMessage;
    public GameObject panel;

    public string[] playerNames = { "player_1", "Player_2" };
    private void Awake () {
        instance = this;
    }

    public void EndGame(){
        int playerId = 0;
        int points = 0;
        foreach (TotalPoints tp in totalPoints)
        {
            int pts = tp.GetPoints();
            if (pts > points){
                points = pts;
                playerId = tp.playerId;
            }
        }
        //display winner
        panel.SetActive(true);
        winnerMessage.text = playerNames[playerId - 1] + " é o vencedor!";
    }
}