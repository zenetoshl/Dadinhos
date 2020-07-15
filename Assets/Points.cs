using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Points", menuName = "Dadinhos/Points", order = 0)]
public class Points : ScriptableObject {
    public int points = 0;
    public int[] indexes; 
}
