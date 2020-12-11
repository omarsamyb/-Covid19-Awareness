using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;
    public bool isPlayerInsideOffice;
    public bool controlsEnabled;
    public bool OpeningSceneEvent;
    public float raycastRange;
    public int interactableMask;
    public int multiChoiceOutcome0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isPlayerInsideOffice = false;
        controlsEnabled = true;
        OpeningSceneEvent = false;
        raycastRange = 8f;
        interactableMask = 1 << 9;
        multiChoiceOutcome0 = -1;
    }
}
