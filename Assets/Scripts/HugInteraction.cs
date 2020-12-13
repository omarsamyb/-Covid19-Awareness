using System.Collections;
using UnityEngine;

public class HugInteraction : Interactable
{
    public Transform npc;
    private Animator playerAnimator;
    private Animator npcAnimator;
    public Transform customInteractionTransform;
    private PlayerMotor playerMotor;
    public override void Interact()
    {
        base.Interact();
        playerAnimator = player.GetComponent<Animator>();
        npcAnimator = npc.GetComponent<Animator>();
        playerMotor = player.GetComponent<PlayerMotor>();
        playerMotor.MoveToPoint(customInteractionTransform.position);
        StartCoroutine(AdjustPosition());
    }

    IEnumerator AdjustPosition()
    {
        while (!playerMotor.arrived)
        {
            yield return null;
        }
        playerAnimator.SetTrigger("isHugging");
        npcAnimator.SetTrigger("isHugging");
        GameManager.instance.OpeningSceneEvent = 1;
        StartCoroutine(WaitForAnimation());
        GameManager.instance.controlsEnabled = false;
    }

    IEnumerator WaitForAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging") || !npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging"))
        {
            yield return null;
        }
        while ((playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging") && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f) || (npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging") && npcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f))
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
