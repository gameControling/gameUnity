using UnityEngine;

public class Ufo_attack : StateMachineBehaviour
{
    private LayerMask attackMask;

    private float attackRate = 100f;

    ControlEnemyAnimation controlEnemyAnimation;
    Rigidbody2D rb;
    private Transform player;
    private Transform firePoint;

    [SerializeField] private GameObject bullet;

    float posPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation = animator.GetComponent<ControlEnemyAnimation>();
        rb = animator.GetComponent<Rigidbody2D>();
        attackMask = LayerMask.GetMask("LayerUser");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        firePoint = animator.GetComponent<Transform>();
        posPlayer = player.position.x;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackRate > 0)
        {
            attackRate -= 1;
        }
        else
        {
            Vector3 fire = firePoint.position;
            if (posPlayer > rb.position.x)
            {
                fire.x += 1.5f;
            }
            else
            {
                fire.x -= 1.5f;
            }
            Debug.Log("ufo bullet");
            Instantiate(bullet, fire, firePoint.rotation);
            controlEnemyAnimation.Move();
            attackRate = 100;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation.ResetMove();
    }
}
