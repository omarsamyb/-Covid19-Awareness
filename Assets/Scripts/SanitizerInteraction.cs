using System.Collections;
using UnityEngine;

public class SanitizerInteraction : Interactable
{

    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        // timer 1 second 

        GameManager.instance.controlsEnabled = true;


    }
}
