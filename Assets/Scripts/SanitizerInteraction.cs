using System.Collections;
using UnityEngine;

public class SanitizerInteraction : Interactable
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
        GameManager.instance.SanitizingEvent = 1;
        playerAnimator.SetBool("isSanitizing", true);
        StartCoroutine(Wait(4.0f));
    }
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        playerAnimator.SetBool("isSanitizing", false);
        GameManager.instance.controlsEnabled = true;
        OutcomeManager.instance.Disable_SanitizeInteraction();
    }
}
