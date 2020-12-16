using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistancing : MonoBehaviour
{
    public BoxCollider socialDistanceCollider;
    public float x = 1.5f;
    public float y = 4.0f;
    public float z = 3.0f;

    void Update()
    {
        socialDistanceCollider.size = new Vector3(x, y, z);
    }
    void OnTriggerEnter(Collider collider) { 
        if(collider.gameObject.CompareTag("Player")){
            GameManager.instance.SocialDistanceCounter = GameManager.instance.SocialDistanceCounter + 1;
            Debug.Log("Had Contact With "+GameManager.instance.SocialDistanceCounter+" People");
        }

    }

}
