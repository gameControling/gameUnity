using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damage = 100;
    private float destroyBullet = 40f;
    SpriteRenderer rbSprite;
    [SerializeField] GameObject boom;

    void Start()
    {
        rbSprite = GetComponent<SpriteRenderer>();
        if(FindFirstObjectByType<move_player>().retDerOfMove())
        {
            rb.linearVelocity = transform.right * speed;
            rbSprite.flipX = false;
        }
        else
        {
            rb.linearVelocity = transform.right * -speed;
            rbSprite.flipX = true;
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
        Debug.Log("object");
        if (collision.GetComponent<Boss_logic>() != null)
        {
            Boss_logic boss = collision.GetComponent<Boss_logic>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Debug.Log("boss");
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.GetComponent<enemy_logic>() != null)
        {
            enemy_logic enemy = collision.GetComponent<enemy_logic>();
            enemy.TakeDamage(damage);
            
            Debug.Log("enemy");
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Vall")
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
