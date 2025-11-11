using UnityEngine;

public class ControlBossAnimation : MonoBehaviour
{
    private Animator animator;
    public float numAtk; 

    public bool isLife { private get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isLife = true;
        animator.SetFloat("numAtk", numAtk);
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
        animator.SetTrigger("atack");
    }

    public void ResetAttack()
    {
        animator.ResetTrigger("atack");
    }

    public void Move()
    {
        animator.SetTrigger("move");
    }

    public void ResetMove()
    {
        animator.ResetTrigger("move");
    }

    public void TriggerNumAtk(int numAttack)
    {
        if (numAttack == 1) 
        {
            animator.SetTrigger("firstAtk");  
        }
        else if (numAttack == 2)
        {
            animator.SetTrigger("secondAtk");
        }
        else if (numAttack == 3)
        {
            animator.SetTrigger("thirdAtk");
        }
    }

    public void ResetNumAtk(int numAttack)
    {
        if (numAttack == 1)
        {
            animator.ResetTrigger("firstAtk");
        }
        else if (numAttack == 2)
        {
            animator.ResetTrigger("secondAtk");
        }
        else if (numAttack == 3)
        {
            animator.ResetTrigger("thirdAtk");
        }
    }
}
