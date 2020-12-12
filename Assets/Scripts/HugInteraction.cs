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
        StartCoroutine(WaitForAnimation());
        GameManager.instance.controlsEnabled = false;
    }

    IEnumerator WaitForAnimation()
    {
        while (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging") || !npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging"))
        {
            yield return null;
        }
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging") || npcAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hugging"))
        {
            yield return null;
        }
        finished = true;
    }
}
