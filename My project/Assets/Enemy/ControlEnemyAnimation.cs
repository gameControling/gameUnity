using UnityEngine;

public class ControlEnemyAnimation : MonoBehaviour
{
    private Animator animator;

    public bool isLife { private get; set; }

    public bool isFlip { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isLife = true;
    }

    private void Update()
    {
        animator.SetBool("life", isLife);
    }

    public void Damage()
    {
        animator.SetTrigger("damage");
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void ResetAttack()
    {
        animator.ResetTrigger("attack");
    }

    public void Move()
    {
        animator.SetTrigger("move");
    }

    public void ResetMove()
    {
        animator.ResetTrigger("move");
    }
}
