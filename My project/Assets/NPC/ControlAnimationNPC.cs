using UnityEngine;

public class ControlAnimationNPC : MonoBehaviour
{
    private Animator animator;

    public bool nearEnemy { get; set; }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        animator.SetBool("nearEnemy", nearEnemy);
    }

    public void NoNear()
    {
        animator.SetTrigger("noNear");
    }
}
