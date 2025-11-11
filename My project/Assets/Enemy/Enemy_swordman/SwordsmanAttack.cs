using UnityEngine;

public class SwordsmanAttack : StateMachineBehaviour
{
    private float attackRate = 2f;
    private float nextAttackTime = 0f;

    ControlEnemyAnimation controlEnemyAnimation;

    public bool isAttack = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation = animator.GetComponent<ControlEnemyAnimation>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttack = true;
        if (Time.time > nextAttackTime)
        {
            Debug.Log("swordsman attack");
            controlEnemyAnimation.Move();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation.ResetMove();
        isAttack = false;
    }
}
