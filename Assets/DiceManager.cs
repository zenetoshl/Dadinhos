using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    public Dice[] die;
    private int i;
    public int ndie = 5;
    public int nPlayers = 1;
    public PlayerScores[] players;
    public static bool rolling = true;
    public bool changed = true;
    public static bool reseted = true;

    private void Start () {
        LockManager.instance.OnNewTurn += NewTurn;
        i = 0;
        players = new PlayerScores[nPlayers];
        for (int i = 0; i < nPlayers; i++) {
            players[i] = ScriptableObject.CreateInstance<PlayerScores> () as PlayerScores;
        }
    }
    // Start is called before the first frame update
    private void FixedUpdate () {
        if (rolling) {
            die[i].Roll ();
            die[(i + 2) % ndie].Roll ();
            die[(i + 4) % ndie].Roll ();
            i = (i + 1) % ndie;
        }
    }

    public static void ChangeRollStatus () {
        rolling = !rolling;
        if(!rolling) reseted = false;
    }

    public void OrganizeDie () {
        SortDie ();
    }

    public void SortDie () {
        Dice[] newDie = new Dice[die.Length];
        int i = 0;
        foreach (Dice d in die.OrderBy (e => e.index)) {
            newDie[i] = d;
            i++;
        }
        die = newDie;
    }

    public void NewTurn(){
        reseted = true;
        foreach(Dice d in die){
            d.Unlock();
        }
    }

    public Points GetNumber (int num) {
        int points = 0;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            if (d.GetFace () == num) {
                points += d.GetFace ();
                indexes.Add (d.index);
            }
        }
        Debug.Log (num + ": " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }

    public Points GetAny () {
        int points = 0;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            points += d.GetFace ();
            indexes.Add (d.index);
        }
        Debug.Log ("Any: " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }

    public Points GetDoublePair () {
        int points = 0;
        int prevFaces = 0;
        int pair = 0;
        int lastIndex = -1;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            points = points + d.GetFace ();
            if (d.GetFace () == prevFaces) {
                indexes.Add (lastIndex);
                indexes.Add (d.index);
                pair += 1;
                prevFaces = 0;
            } else {
                prevFaces = d.GetFace ();
                lastIndex = d.index;
            }
        }
        if (pair != 2) {
            points = 0;
            indexes = new List<int> ();
        }
        Debug.Log ("Double Pairs: " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }

    public Points GetFullHouse () {
        int points = 0;
        int prevFaces = 0;
        int pair = 0;
        int triple = 0;
        bool paired = false;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            indexes.Add (d.index);
            if (d.GetFace () == prevFaces) {
                if (paired) {
                    triple++;
                    pair--;
                    points = points + d.GetFace ();
                } else {
                    paired = true;
                    pair++;
                    points = points + (d.GetFace () * 2);
                }
            } else {
                paired = false;
            }
            prevFaces = d.GetFace ();
        }
        if (pair != 1 || triple != 1) {
            points = 0;
            indexes = new List<int> ();
        }
        Debug.Log ("Full House: " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }

    public Points GetRepetitions (int num) {
        int points = 0;
        int prevFaces = 0;
        int sequence = 0;
        bool sequenceFound = false;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            points = points + d.GetFace ();
            if (!sequenceFound) {
                indexes.Add (d.index);
            }
            if (d.GetFace () == prevFaces) {
                sequence++;
                if (sequence + 1 == num) {
                    sequenceFound = true;
                }
            } else {
                sequence = 0;
                if (!sequenceFound) {
                    indexes = new List<int> ();
                }
            }
            prevFaces = d.GetFace ();
        }
        if (!sequenceFound) {
            points = 0;
            indexes = new List<int> ();
        }
        Debug.Log ("Repetition of " + num + ": " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }

    public Points GetSequences (int size) {
        int points = 0;
        int prevFaces = -10;
        int sequence = 0;
        bool sequenceFound = false;
        List<int> indexes = new List<int> ();
        foreach (Dice d in die.OrderBy (e => e.GetFace ())) {
            points = points + d.GetFace ();
            if (!sequenceFound) {
                indexes.Add (d.index);
            }
            if (d.GetFace () == prevFaces + 1) {
                sequence++;
                if (sequence + 1 == size) {
                    sequenceFound = true;
                }
            } else {
                sequence = 0;
                if (!sequenceFound) {
                    indexes = new List<int> ();
                }
            }
            prevFaces = d.GetFace ();
        }
        if (!sequenceFound) {
            indexes = new List<int> ();
            points = 0;
        }
        Debug.Log ("Sequence size " + size + ": " + points);
        Points returnPoints = new Points ();
        returnPoints.points = points;
        returnPoints.indexes = indexes;
        return returnPoints;
    }
}