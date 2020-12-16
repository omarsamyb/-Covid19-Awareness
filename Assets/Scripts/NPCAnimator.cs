using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    public string boolName;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(boolName, true);
    }
}
