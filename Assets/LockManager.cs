using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public static UIPoints selected = null;
    public DiceManager dm;

    public static LockManager instance;
    public event Action OnNewTurn;

    private void Awake () {
        instance = this;
    }

    public void Lock(){
        if(selected == null) return;
        Debug.Log("lockado?");
        selected.Lock();
        selected = null;
        if (OnNewTurn != null) {
                OnNewTurn ();
        }
    }

    public static void Select(UIPoints newSelect){
        if(selected != null) selected.Unselect();
        selected = newSelect;
    }
}
