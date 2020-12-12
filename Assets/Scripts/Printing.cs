using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printing : Interactable
{
  private Animator playerAnimator;
    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        playerAnimator = player.GetComponent<Animator>();
       // Vector3 newPosition = new Vector3(playerPositionX, 1.0F, playerPositionZ);
       // player.transform.position = newPosition;
       // player.transform.rotation

        playerAnimator.SetBool("isPrinting", true);
        StartCoroutine(Wait(21.0f));
    }
    private IEnumerator Wait(float waitTime)
    {

            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
            playerAnimator.SetBool("isPrinting", false);
            GameManager.instance.controlsEnabled = true;
    }
}
