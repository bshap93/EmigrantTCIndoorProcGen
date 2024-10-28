using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSystemsManager : MonoBehaviour
{
    static CoreSystemsManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}