using System.Collections;
using UnityEngine;

public class chairInteraction : Interactable
{
    private Animator playerAnimator;
    public Transform directionTransform;


    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        playerAnimator = player.GetComponent<Animator>();
        StartCoroutine(Wait(10.0f));
    }
    private IEnumerator Wait(float waitTime)
    {
        Quaternion initialRotation = player.rotation;
        Vector3 direction = (directionTransform.position - interactionTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        for (float t = 0f; t < 1f; t += 3f * Time.deltaTime)
        {
            player.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);
            yield return null;
        }
        direction = (directionTransform.position - interactionTransform.position).normalized;
        player.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        playerAnimator.SetBool("isSittingPc", true);
        //yield return new WaitForSeconds(1.0f);
        float savedLocation = player.position.z;
        float diff = player.position.z - transform.position.z;
        Vector3 chairInitialPos = transform.position;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z + diff);
        for(float t = 0f; t<0.7f; t += 2f * Time.deltaTime)
        {
            transform.position = Vector3.Slerp(chairInitialPos, target, t);
            yield return null;
        }
        Vector3 playerInitialPos = player.position;
        chairInitialPos = transform.position;
        target = player.position;
        target.z = 13.2f;
        diff = player.position.z - transform.position.z;

        for (float t = 0; t<1; t += 1f * Time.deltaTime)
        {
            player.position = Vector3.Slerp(playerInitialPos, target, t);
            //transform.position = Vector3.Slerp(chairInitialPos, target, t);
            transform.position = new Vector3(chairInitialPos.x, chairInitialPos.y, player.position.z - diff);
            yield return null;
        }
        player.position = new Vector3(player.position.x, 0.18f, player.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - diff);

        playerAnimator.SetBool("isTyping", true);
        AudioManager.instance.Play("typingSFX");
        player.position = new Vector3(player.position.x, player.position.y, player.position.z - 0.2f);
        GameManager.instance.LaptopTask = true;
        yield return new WaitForSeconds(waitTime);
        player.position = new Vector3(player.position.x, 0, player.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);

        playerAnimator.SetBool("isTyping", false);
        AudioManager.instance.Stop("typingSFX");
        for (float t = 0; t < 1; t += 1f * Time.deltaTime)
        {
            player.position = Vector3.Slerp(target, playerInitialPos, t);
            transform.position = new Vector3(chairInitialPos.x, chairInitialPos.y, player.position.z - diff);
            yield return null;
        }
        playerAnimator.SetBool("isTyping", false);
        playerAnimator.SetBool("isSittingPc", false);
        chairInitialPos = transform.position;
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
        for (float t = 0f; t < 1f; t += 2f * Time.deltaTime)
        {
            transform.position = Vector3.Slerp(chairInitialPos, target, t);
            yield return null;
        }
        GameManager.instance.controlsEnabled = true;
        OutcomeManager.instance.Disable_DeskInteraction();
    }
}
