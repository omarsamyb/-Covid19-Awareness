using System.Collections;
using UnityEngine;

public class WaveInteraction : Interactable
{
    public Transform npc;
    private Animator playerAnimator;
    private Animator npcAnimator;

    public override void Interact()
    {
        base.Interact();
        playerAnimator = player.GetComponent<Animator>();
        npcAnimator = npc.GetComponent<Animator>();
        playerAnimator.SetTrigger("isWaving");
        npcAnimator.SetTrigger("isWaving");
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving") || !npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving"))
        {
            yield return null;
        }
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving") || npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving"))
        {
            yield return null;
        }
        finished = true;
    }
}
