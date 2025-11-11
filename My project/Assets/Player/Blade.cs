using UnityEngine;

public class Blade : MonoBehaviour
{
    float dirAttack;
    int damage = 20;
    SpriteRenderer spriteRenderer;
    float timerDestroy = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {       
        if (FindFirstObjectByType<move_player>().retDerOfMove())
        {
            dirAttack = 1;
            spriteRenderer.flipX = false;
        }
        else
        {
            dirAttack = -1;
            spriteRenderer.flipX = true;
        }
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(dirAttack, 0);
        if(timerDestroy > 0)
        {
            timerDestroy -= Time.deltaTime;
        }
        else
        {
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
            Destroy(gameObject);
        }
        else if (collision.GetComponent<enemy_logic>() != null)
        {
            enemy_logic enemy = collision.GetComponent<enemy_logic>();
            enemy.TakeDamage(damage);

            Debug.Log("enemy");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Vall")
        {
            Destroy(gameObject);
        }
    }
}
