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
        if(GameManager.instance.CheckOutEvent){
            SceneManager.LoadScene(reportScene);
        }else{
            StartCoroutine(WaitForAnimation());
        }


    }
    IEnumerator WaitForAnimation(){
        GameManager.instance.controlsEnabled = true;
        yield return null;

    }

}

