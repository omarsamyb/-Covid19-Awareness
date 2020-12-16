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
        GameManager.instance.OpeningSceneEvent = 0;
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving") || !npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving"))
        {
            yield return null;
        }
        while ((playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving") && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f) || (npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Waving") && npcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f))
        {
            yield return null;
        }
        playerAnimator.SetBool("isTalking", true);
        npcAnimator.SetBool("isTalking", true);
        while (AudioManager.instance.isPlaying("GreetingsSFX"))
            yield return null;
        playerAnimator.SetBool("isTalking", false);
        npcAnimator.SetBool("isTalking", false);
        finished = true;
    }
}
