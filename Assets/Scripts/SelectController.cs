using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public UIPoints uiPoint;
    public void Select() {
        uiPoint.Select();
        Debug.Log("ola1");
    }
}
