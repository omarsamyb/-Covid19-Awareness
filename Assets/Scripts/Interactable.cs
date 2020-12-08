using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    private bool isFocused;
    private Transform player;
    private bool hasInteracted;
    public Transform interactionTransform;

    private void Start()
    {
        radius = 3f;
        isFocused = false;
        hasInteracted = false;
    }
    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
        
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocused = false;
        player = null;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
