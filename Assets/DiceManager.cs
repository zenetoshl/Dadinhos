using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : MonoBehaviour {
        public Dice[] dices;
        private int i;
        public int nDices = 5;
        public int nPlayers = 1;
        public PlayerScores[] players;
        public static bool rolling = true;
        public bool changed = true;

        private void Start () {
            i = 0;
            players = new PlayerScores[nPlayers];
            for (int i = 0; i < nPlayers; i++) {
                players[i] = ScriptableObject.CreateInstance<PlayerScores> () as PlayerScores;
            }
        }
        // Start is called before the first frame update
        private void FixedUpdate () {
            if (rolling) {
                dices[i].Roll ();
                dices[(i + 2) % nDices].Roll ();
                dices[(i + 4) % nDices].Roll ();
                i = (i + 1) % nDices;
            } else if (changed) {
                changed = false;
                for (int i = 0; i < 6; i++) {
                    GetNumber (i + 1);
                }
                for (int i = 0; i < 2; i++) {
                    GetSequences (4 + i);
                }
                for (int i = 3; i < 6; i++) {
                    GetRepetitions (i);
                }
                GetDoublePair ();
                GetFullHouse ();
            }
        }

        public void ChangeRollStatus () {
            rolling = !rolling;
        }

        public int GetNumber (int num) {
            int points = 0;
            foreach (Dice d in dices.OrderBy (e => e.GetFace ())) {
                if (d.GetFace () == num) {
                    points += num;
                }
            }
            Debug.Log (num + ": " + points);
            return points;
        }

        public int GetDoublePair () {
            int points = 0;
            int prevFaces = 0;
            int pair = 0;
            foreach (Dice d in dices.OrderBy (e => e.GetFace ())) {
                if (d.GetFace () == prevFaces) {
                    pair += 1;
                    prevFaces = 0;
                    points = points + (d.GetFace () * 2);
                } else {
                    prevFaces = d.GetFace ();
                }
            }
            if (pair != 2) {
                points = 0;
            }
            Debug.Log ("Double Pairs: " + points);
            return points;
        }

        public int GetFullHouse () {
            int points = 0;
            int prevFaces = 0;
            int pair = 0;
            int triple = 0;
            bool paired = false;
            foreach (Dice d in dices.OrderBy (e => e.GetFace ())) {
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
                    prevFaces = d.GetFace ();
                }
            }
            if (pair != 1 || triple != 1) {
                points = 0;
            }
            Debug.Log ("Full House: " + points);
            return points;
        }

        public int GetRepetitions (int num) {
            int points = 0;
            int prevFaces = 0;
            int sequence = 0;
            foreach (Dice d in dices.OrderBy (e => e.GetFace ())) {
                if (d.GetFace () == prevFaces) {
                    sequence++;
                    if (sequence + 1 == num) {
                        points = d.GetFace () * num;
                    }
                } else {
                    sequence = 0;
                    prevFaces = d.GetFace ();
                }
            }
            Debug.Log ("Repetition of " + num + ": " + points);
            return points;
        }

        public int GetSequences (int size) {
            int points = 0;
            int prevFaces = 0;
            int sequence = 0;
            foreach (Dice d in dices.OrderBy (e => e.GetFace ())) {
                if (d.GetFace () == prevFaces + 1) {
                    sequence++;
                    if (sequence + 1 == size) {
                        if (d.GetFace () == 4) {
                                points = 1 + 2 + 3 + 4;
                            } else {
                                points = (size == 4) ? (5 + 4 + 3 + 2) : (5 + 4 + 3 + 2 + 1);
                            }
                        }
                    } else {
                        sequence = 0;
                        prevFaces = d.GetFace ();
                    }
                }
                Debug.Log ("Sequence size " + size + ": " + points);
                return points;
            }
        }