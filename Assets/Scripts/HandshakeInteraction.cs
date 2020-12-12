using System.Collections;
using UnityEngine;

public class HandshakeInteraction : Interactable
{
    public Transform npc;
    private Animator playerAnimator;
    private Animator npcAnimator;

    public override void Interact()
    {
        base.Interact();
        playerAnimator = player.GetComponent<Animator>();
        npcAnimator = npc.GetComponent<Animator>();
        playerAnimator.SetTrigger("isShakingHands");
        npcAnimator.SetTrigger("isShakingHands");
        StartCoroutine(WaitForAnimation());
    }
    IEnumerator WaitForAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ShakingHands") || !npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ShakingHands"))
        {
            yield return null;
        }
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ShakingHands") || npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ShakingHands"))
        {
            yield return null;
        }
        finished = true;
    }
}
