using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TVInteraction : Interactable
{
    public Transform directionTransform;
    private Animator playerAnimator;
    public GameObject playerVC;
    public GameObject tvVC;
    public Transform crosshair;
    public GameObject videoFX;
    private VideoPlayer vp;
    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        playerAnimator = player.GetComponent<Animator>();
        vp = videoFX.GetComponent<VideoPlayer>();
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
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
        StartCoroutine(WatchTV());
    }
    IEnumerator WatchTV()
    {
        yield return new WaitForSeconds(2);
        tvVC.SetActive(true);
        playerVC.SetActive(false);
        crosshair.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        videoFX.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(WaitForVideo());
    }
    IEnumerator WaitForVideo()
    {
        while (vp.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
                break;
            yield return null;
        }
        videoFX.SetActive(false);
        yield return new WaitForSeconds(1);
        playerAnimator.SetBool("isSitting", false);
        GameManager.instance.controlsEnabled = true;
        playerVC.SetActive(true);
        tvVC.SetActive(false);
        crosshair.gameObject.SetActive(true);
        finished = true;
        OutcomeManager.instance.Disable_TvInteraction();
    }
}
