using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteraction : Interactable
{
    public Transform directionTransform;
    private Animator playerAnimator;
    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        playerAnimator = player.GetComponent<Animator>();
        StartCoroutine(WatchTV());
    }

    IEnumerator WatchTV()
    {
        Quaternion initialRotation = player.rotation;
        Vector3 direction = (directionTransform.position - interactionTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        for (float t = 0f; t < 1f; t += 4f * Time.deltaTime)
        {
            player.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);
            yield return null;
        }
        direction = (directionTransform.position - interactionTransform.position).normalized;
        player.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        playerAnimator.SetBool("isSitting", true);
        yield return new WaitForSeconds(3);
        playerAnimator.SetBool("isSitting", false);
        playerAnimator.SetTrigger("isStanding");
    }
}
