using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScanner : Interactable
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

        playerAnimator.SetBool("isScanning", true);
        StartCoroutine(Wait(6.5f));
    }
    private IEnumerator Wait(float waitTime)
    {
        GameManager.instance.CheckOutEvent = true;
        AudioManager.instance.Play("CheckoutSFX");
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
        playerAnimator.SetBool("isScanning", false);
        GameManager.instance.controlsEnabled = true;
        OutcomeManager.instance.Disable_AttendanceInteraction();
    }
}
