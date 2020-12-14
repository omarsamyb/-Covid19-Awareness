using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : Interactable
{
    public string reportScene;

    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        AudioManager.instance.Stop("BackgroundSFX");
        SceneManager.LoadScene(reportScene);
    }
}

