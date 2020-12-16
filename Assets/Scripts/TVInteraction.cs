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
    public Transform numberOfNpc;
    private int n;
    public GameObject objectiveUI;

    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        playerAnimator = player.GetComponent<Animator>();
        vp = videoFX.GetComponent<VideoPlayer>();
        n = Mathf.FloorToInt(numberOfNpc.localPosition.x);
        objectiveUI.SetActive(false);
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
        videoFX.SetActive(true);
        while (!vp.isPlaying)
        {
            yield return null;
        }
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
        if (n == 2)
            GameManager.instance.EntertainmentEvent = 0;
        else
            GameManager.instance.EntertainmentEvent = 1;
        objectiveUI.SetActive(true);
    }
}
