using UnityEngine;

public class Npc_near_enemy : StateMachineBehaviour
{
    Rigidbody2D rb;
    LayerMask enemyMask;
    float rangeNearEnemy = 10f;
    ControlAnimationNPC npc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<ControlAnimationNPC>();
        rb = animator.GetComponent<Rigidbody2D>();
        enemyMask = LayerMask.GetMask("LayerEnemy");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D[] colInfo = Physics2D.OverlapCircleAll(rb.position, rangeNearEnemy, enemyMask);

        if(colInfo != null)
        {
            if (colInfo.Length == 0)
            {

                npc.NoNear();
                npc.nearEnemy = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
