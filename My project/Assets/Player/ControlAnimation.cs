using UnityEngine;
using UnityEngine.Rendering;

public class ControlAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool isMove { get;  set; }
    public bool isFly { get; set; }

    public bool onWall { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("isMove", isMove);
        animator.SetBool("isFly", isFly);
        animator.SetBool("onWall", onWall);
    }

    public void Jump()
    {
        if (animator)
        {
            Debug.Log("jump");
            animator.SetTrigger("jump");
        }
    }

    public void Attack()
    {
        if (animator)
        {
            animator.SetTrigger("attack");
        }
    }

    public void Fire()
    {
        if(animator)
        {
            animator.SetTrigger("fire");
        }       
    }

    public void Desh()
    {
        if (animator)
        {
            animator.SetTrigger("desh");
        }
    }
}
