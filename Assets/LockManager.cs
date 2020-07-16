using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public static UIPoints selected = null;

    public void Lock(){
        if(selected != null){
            selected.Lock();
        }
        //new roll
    }
}
