using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damage = 10;
    Transform player;
    Vector2 target;
    private float destroyBullet = 80f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (player.position - rb.transform.position).normalized;
        if (target == Vector2.zero)
        {
            rb.linearVelocity = transform.right * speed;
        }
        else
        {
            rb.linearVelocity = transform.right * -speed;
        }
        Debug.Log("create bullet");
    }

    private void Update()
    {
        if (destroyBullet > 0)
        {
            destroyBullet -= 1;
        }
        else
        {
            Debug.Log("delete bullet");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("delete bullet");
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            move_player boss = collision.GetComponent<move_player>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject == GameObject.FindGameObjectWithTag("Vall"))
        {
            Destroy(gameObject);
        }
    }
}
