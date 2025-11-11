using Unity.VisualScripting;
using UnityEngine;

public class Slime_attack : StateMachineBehaviour
{
    private float attackDamage = 20;

    private float attackRange = 2f;
    private LayerMask attackMask;

    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    ControlEnemyAnimation controlEnemyAnimation;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation = animator.GetComponent<ControlEnemyAnimation>();
        rb = animator.GetComponent<Rigidbody2D>();
        attackMask = LayerMask.GetMask("LayerUser");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > nextAttackTime)
        {
            Debug.Log("slime atack");

            Collider2D colInfo = Physics2D.OverlapCircle(rb.position, attackRange, attackMask);
            if (colInfo != null)
            {
                if(colInfo.GetComponent<move_player>())
                {
                    colInfo.GetComponent<move_player>().TakeDamage(attackDamage);
                }
            }
            controlEnemyAnimation.Move();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation.ResetMove();
    }
}
