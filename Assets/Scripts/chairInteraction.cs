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
        StartCoroutine(Wait(4.0f));
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
        yield return new WaitForSeconds(2.0f);
        float savedLocation = player.position.z;
        for (; player.position.z < 13.2f;)
        {
            player.position = new Vector3(player.position.x, player.position.y, player.position.z + 0.005f);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.005f);
            yield return null;
        }
        player.position = new Vector3(player.position.x, 0.18f, player.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.1f);

        playerAnimator.SetBool("isTyping", true);
        GameManager.instance.LaptopTask = true;
        yield return new WaitForSeconds(waitTime);
        player.position = new Vector3(player.position.x, 0, player.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);

        playerAnimator.SetBool("isTyping", false);
        for (; player.position.z > savedLocation;)
        {
            player.position = new Vector3(player.position.x, player.position.y, player.position.z - 0.005f);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.005f);
            yield return null;
        }
        playerAnimator.SetBool("isTyping", false);
        playerAnimator.SetBool("isSittingPc", false);
        GameManager.instance.controlsEnabled = true;
    }
}
