using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexSaveSound : MonoBehaviour
{
    public static AlexSaveSound instance;
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
