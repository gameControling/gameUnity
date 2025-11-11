using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_third_attack : StateMachineBehaviour
{
    private float attackDamage = 20;

    private float attackRange = 2f;
    private LayerMask attackMask;

    ControlBossAnimation controlBossAnimation;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation = animator.GetComponent<ControlBossAnimation>();
        rb = animator.GetComponent<Rigidbody2D>();
        attackMask = LayerMask.GetMask("LayerUser");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D colInfo = Physics2D.OverlapCircle(rb.position, attackRange, attackMask);
        if (colInfo != null)
        {
            if (colInfo.GetComponent<move_player>())
            {
                colInfo.GetComponent<move_player>().TakeDamage(attackDamage);
            }
        }

        controlBossAnimation.Move();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation.ResetMove();
    }
}
