using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = audioSource.clip;
        if (!GameManager.instance.sceneInProgress)
            audioSource.PlayOneShot(clip);
    }
}
