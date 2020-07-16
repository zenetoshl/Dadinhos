using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public UIPoints uiPoint;
    private void OnMouseUp() {
        uiPoint.Select();
        Debug.Log("ola1");
    }
}
