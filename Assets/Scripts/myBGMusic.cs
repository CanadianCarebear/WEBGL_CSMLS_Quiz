using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBGMusic : MonoBehaviour
{
    private static myBGMusic _instance;

    private void Awake()
    {
        if (!_instance)
        { _instance = this; }
        //otherwise, if we do, kill this thing
        else
        { Destroy(this.gameObject); }

        DontDestroyOnLoad(this.gameObject);
    }
}
