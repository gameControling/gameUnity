using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_run : StateMachineBehaviour
{
    UnityEngine.Transform player;
    Rigidbody2D rb;
    float speed = 5f;
    Boss_logic boss;
    ControlBossAnimation controlBossAnimation;
    float timerNextNumAttack = 2f;
    int TriggerNumAtk = 1;
    LayerMask attackMask;
    float damage = 20;
    float range = 2;
    [SerializeField] GameObject dirAttack;
    float timerFirstAttack = 1f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss_logic>();
        controlBossAnimation = animator.GetComponent<ControlBossAnimation>();
        rb.rotation = 0;
        rb.gravityScale = 1;
        animator.GetComponent<BoxCollider2D>().isTrigger = false;
        attackMask = LayerMask.GetMask("LayerUser");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        boss.lookAtPlayer();
        if(rb.linearVelocityY == 0)
        {
            if(timerNextNumAttack > 0)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.position = newPos;
                timerNextNumAttack -= Time.deltaTime;
            }
            else
            {
                if(TriggerNumAtk == 0)
                {
                    TriggerNumAtk = Random.Range(1, 3);
                }
                else if(TriggerNumAtk == 1)
                {
                    if (timerFirstAttack > 0)
                    {
                        Instantiate(dirAttack, animator.transform.position, animator.transform.rotation);
                        timerFirstAttack -= Time.deltaTime;
                    }
                    else
                    {
                        controlBossAnimation.TriggerNumAtk(TriggerNumAtk);
                        timerNextNumAttack = 2f;
                        timerFirstAttack = 1f;
                        TriggerNumAtk = 0;
                    }   
                }
                else
                {
                    controlBossAnimation.TriggerNumAtk(TriggerNumAtk);
                    timerNextNumAttack = 2f;
                    TriggerNumAtk = 0;
                }        
            }
        }
        
        Collider2D colInfo = Physics2D.OverlapCircle(rb.position, range, attackMask);
        if (colInfo != null)
        {
            if (colInfo.GetComponent<move_player>())
            {
                colInfo.GetComponent<move_player>().TakeDamage(damage);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation.ResetMove();
        controlBossAnimation.ResetNumAtk(TriggerNumAtk);
    }
}
