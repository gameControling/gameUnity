using UnityEngine;

public class Boss_first_attack : StateMachineBehaviour
{
    ControlBossAnimation controlBossAnimation;
    bool moveLeft;
    float speed = 10;
    SpriteRenderer spriteRenderer;
    float damage = 20;
    private LayerMask mask;
    Rigidbody2D rb;
    Transform player;
    [SerializeField] GameObject pointCenter;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation = animator.GetComponent<ControlBossAnimation>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        mask = LayerMask.GetMask("LayerUser");
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        moveLeft = player.position.x > animator.transform.position.x ? false : true;
        rb.rotation = player.position.x > animator.transform.position.x ?  90 : -90 ;
        rb.gravityScale = 0;

        rb.linearVelocity = new Vector2(0, 0);
        animator.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("boss atack");

        if(moveLeft)
        {
            spriteRenderer.flipX = false;
            animator.transform.position -= new Vector3(1, 0) * speed * Time.deltaTime;
        }
        else
        {
            spriteRenderer.flipX = true;
            animator.transform.position += new Vector3(1, 0) * speed * Time.deltaTime;
        }
        Collider2D colInfo = Physics2D.OverlapCircle(rb.position, 2, mask);
        if (colInfo != null)
        {
            if (colInfo.GetComponent<move_player>())
            {
                colInfo.GetComponent<move_player>().TakeDamage(damage);
            }
        }
        if (rb.position.x > pointCenter.transform.position.x + 40 || rb.position.x < pointCenter.transform.position.x - 40 )
        {
            controlBossAnimation.Move();
            animator.transform.position = new Vector2(player.position.x, pointCenter.transform.position.y + 40);
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controlBossAnimation.ResetMove();
    }
}
