using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;
    public bool isPlayerInsideOffice;
    public bool controlsEnabled;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isPlayerInsideOffice = false;
        controlsEnabled = true;
    }
}
