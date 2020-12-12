using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius;
    private bool isFocused;
    protected Transform player;
    private bool hasInteracted;
    public Transform interactionTransform;
    public bool finished;

    private void Start()
    {
        radius = 1f;
        isFocused = false;
        hasInteracted = false;
        finished = false;
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
        finished = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }


    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
