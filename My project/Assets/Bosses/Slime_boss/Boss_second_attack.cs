using UnityEngine;

public class Boss_second_attack : StateMachineBehaviour
{
    private float attack_timer = 0.3f;

    ControlBossAnimation controlBossAnimation;
    [SerializeField] GameObject slimeBall;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation = animator.GetComponent<ControlBossAnimation>();
        attack_timer = 0.4f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0 > attack_timer)
        {
            Debug.Log("boss atack");
            Instantiate(slimeBall, animator.transform.position, animator.transform.rotation);
            controlBossAnimation.Move();
            attack_timer = 0.4f;
        }
        else
        {
            attack_timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation.ResetMove();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
