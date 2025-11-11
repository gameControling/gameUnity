using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SlimeBall : MonoBehaviour
{
    float speed = 15f;
    float damage = 20;
    Animator animator;
    Rigidbody2D rb;
    float rotation = 0;
    bool timerDieStart = false;
    float timer = 0.4f;
    [SerializeField] GameObject boom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player.position.x > rb.transform.position.x)
        {
            rb.linearVelocity = new Vector2(speed, 10);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotation += 2;
        transform.rotation = new Quaternion(0, 0, rotation, 0);
        if (timerDieStart) 
        {
            if(timer < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetTrigger("die");
            collision.GetComponent<move_player>().TakeDamage(damage);
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            timerDieStart = true;
            timer = 0.4f;
            Instantiate(boom, transform.position, transform.rotation);
        }
        else if (collision.tag == "Vall")
        { 
            animator.SetTrigger("die");
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            timerDieStart = true;
            timer = 0.4f;
            Instantiate(boom, transform.position, transform.rotation);
        }
        
    }
}
