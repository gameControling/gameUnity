using Unity.Mathematics;
using UnityEngine;

public class Ufo_run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    float speed = 5f;
    enemy_logic ufo;
    float attackRange = 10f;
    ControlEnemyAnimation controlEnemyAnimation;
    private float coldownFire = 80f;
    float coldownMove = 5f;
    bool leftOrRightMove = false;
    bool dirMove = false;
    float coldownUnwrap = 20;
    SpriteRenderer spriteRenderer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        ufo = animator.GetComponent<enemy_logic>();
        controlEnemyAnimation = animator.GetComponent<ControlEnemyAnimation>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation.ResetMove();

        Vector2 newPos;
        Vector2 target;

        if (math.abs(player.position.x - rb.position.x) < attackRange &&
                math.abs(player.position.y - rb.position.y) < 5f)
        {
            ufo.lookAtPlayer();
            target = new Vector2(player.position.x, rb.position.y);
            newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            if (coldownMove == 0)
            {
                if (leftOrRightMove)
                {
                    rb.MovePosition(rb.position + new Vector2(0.2f, -0.1f));
                    spriteRenderer.flipX = true;
                }
                else
                {
                    rb.MovePosition(rb.position + new Vector2(-0.2f, -0.1f));
                    spriteRenderer.flipX = false;
                }
                if (coldownUnwrap == 0)
                {
                    leftOrRightMove = !leftOrRightMove;
                    coldownUnwrap = 20;
                }
                else
                {
                    coldownUnwrap -= 1;
                }
                coldownMove = 5;
            }
            else
            {
                coldownMove -= 1;
            }
        }

        if (coldownFire == 0)
        {
            if (math.abs(player.position.x - rb.position.x) < attackRange &&
                math.abs(player.position.y - rb.position.y) < 0.5f)
            {
                controlEnemyAnimation.Attack();
                coldownFire = 80f;
            }
        }
        else
        {
            coldownFire -= 1;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlEnemyAnimation.ResetAttack();
    }
}
