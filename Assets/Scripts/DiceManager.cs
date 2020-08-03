using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    public List<Dice> die = new List<Dice> ();
    private List<Dice> unselected = new List<Dice> ();
    private List<Dice> selected = new List<Dice> ();
    private int i;
    private int nunselected = 5;
    public int nPlayers = 1;
    public PlayerScores[] players;
    public static bool rolling = false;
    public static bool reseted = true;
    public float unselectedSpacing = 2.7f;
    public float selectedSpacing = 1.8f;
    public float mediumPoint = 0f;
    public float selectStartPoint = -3.6f;

    private float selectedYAxis;

    public static DiceManager instance;

    private void Awake () {
        instance = this;
    }

    public void RemoveFromUnselected (Dice d) {
        unselected.Remove (d);
        selected.Add (d);
        OrganizeUnselected ();
        OrganizeSelected ();
    }

    public void RemoveFromSelected (Dice d) {
        unselected.Add (d);
        selected.Remove (d);
        OrganizeUnselected ();
        OrganizeSelected ();
    }

    private void Start () {
        LockManager.instance.OnNewTurn += NewTurn;
        selectedYAxis = Camera.main.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y - (selectedSpacing / 2);
        initializeUnselected ();
        i = 0;
        players = new PlayerScores[nPlayers];
        for (int i = 0; i < nPlayers; i++) {
            players[i] = ScriptableObject.CreateInstance<PlayerScores> () as PlayerScores;
        }
        OrganizeUnselected ();
        OrganizeSelected ();
    }

    private void initializeUnselected () {
        foreach (Dice d in die) {
            unselected.Add (d);
        }
    }

    private void FixedUpdate () {
        if (rolling) {
            unselected[i].Roll ();
            unselected[(i + 2) % nunselected].Roll ();
            unselected[(i + 4) % nunselected].Roll ();
            i = (i + 1) % nunselected;
        }
    }

    public static void ChangeRollStatus () {
        rolling = !rolling;
        if (!rolling) reseted = false;
    }

    private void OrganizeUnselected () {
        Sortunselected ();
        nunselected = unselected.Count;
        float firstPos = 0f;
        switch (nunselected) {
            case 1:
                firstPos = mediumPoint;
                break;
            case 2:
                firstPos = mediumPoint - (unselectedSpacing / 2);
                break;
            case 3:
                firstPos = mediumPoint - unselectedSpacing;
                break;
            case 4:
                firstPos = mediumPoint - unselectedSpacing - (unselectedSpacing / 2);
                break;
            case 5:
                firstPos = mediumPoint - (unselectedSpacing * 2);
                break;
            default:
                break;
        }
        float nextPos = firstPos;
        foreach (Dice d in unselected) {
            d.transform.localPosition = new Vector3 (nextPos, 0, 0);
            nextPos += unselectedSpacing;
        }
    }

    private void OrganizeSelected () {
        float nextPos = selectStartPoint;
        foreach (Dice d in selected) {
            d.transform.localPosition = new Vector3 (nextPos, selectedYAxis, 0);
            nextPos += selectedSpacing;
        }
    }

    private void Sortunselected () {
        List<Dice> newunselected = new List<Dice> ();
        int i = 0;
        foreach (Dice d in unselected.OrderBy (e => e.index)) {
            newunselected.Add (d);
            i++;
        }
        unselected = newunselected;
    }

    public void NewTurn () {
        reseted = true;
        foreach (Dice d in selected) {
            d.Unlock ();
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