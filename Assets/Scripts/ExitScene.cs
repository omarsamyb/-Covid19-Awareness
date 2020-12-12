using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : Interactable
{
    private Animator playerAnimator;
    public string reportScene;

    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        SceneManager.LoadScene(reportScene);


    }
}

