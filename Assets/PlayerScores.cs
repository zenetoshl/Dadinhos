using UnityEngine;

[CreateAssetMenu (fileName = "PlayerScores", menuName = "Dadinhos/PlayerScores", order = 0)]
public class PlayerScores : ScriptableObject {
    public int one = -1;
    public int two = -1;
    public int three = -1;
    public int four = -1;
    public int five = -1;
    public int six = -1;
    public int doublePair = -1;
    public int triple = -1;
    public int fullHouse = -1;
    public int quadra = -1;
    public int sequenceFour = -1;
    public int sequenceFull = -1;
    public int penta = -1;

    public int GetTotal(){
        return ((one == -1) ? 0 : one) + ((two == -1) ? 0 : two) + ((three == -1) ? 0 : three) + ((four == -1) ? 0 : four) + ((five == -1) ? 0 : five) + ((six == -1) ? 0 : six)
        + ((doublePair == -1) ? 0 : doublePair) + ((triple == -1) ? 0 : triple) + ((fullHouse == -1) ? 0 : fullHouse) + ((quadra == -1) ? 0 : quadra) + ((sequenceFour == -1) ? 0 : sequenceFour)
        + ((sequenceFull == -1) ? 0 : sequenceFull) + ((penta == -1) ? 0 : penta);
    }

}