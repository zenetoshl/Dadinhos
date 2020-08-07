using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsErrorsManager : MonoBehaviour
{
    public static GameObject selectLockedError;

    private void Awake() {
        selectLockedError = GameObject.Find("Canvas/selectLockedError");
    }
}
