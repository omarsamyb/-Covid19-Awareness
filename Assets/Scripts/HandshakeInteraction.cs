using System.Collections;
using System.Collections.Generic;
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
    }
}
