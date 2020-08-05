using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public static UIPoints selected = null;

    public static LockManager instance;
    public event Action OnNewTurn;

    public GameObject errorMessage;

    private void Awake () {
        instance = this;
    }

    public void Lock(){
        if(selected == null) {
            StartCoroutine(ShowErrorMessage());
            return;
        };

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

    private IEnumerator ShowErrorMessage(){
        errorMessage.SetActive(true);
        yield return new WaitForSeconds(5f);
        errorMessage.SetActive(false);
    }
}
