using UnityEngine;

public class enemy_logic : SoundsScript
{
    public Transform player;
    public bool isFlip = false;
    public int maxHealth = 100;
    int currentHealt;
    Rigidbody2D rb;

    ControlEnemyAnimation controlEnemyAnimation;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealt = maxHealth;
        controlEnemyAnimation = GetComponent<ControlEnemyAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocityX != 0)
        {
            PlaySound(0);
        }
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x)
        {
            transform.localScale = flipped;
            spriteRenderer.flipX = false;
            isFlip = false;
        }
        else if (transform.position.x < player.position.x)
        {
            transform.localScale = flipped;
            spriteRenderer.flipX = true;
            isFlip = true;
        }
    }

    public void flipObject(bool leftOrRight)
    {
        if (leftOrRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;

        if(isFlip)
        {
            rb.linearVelocity = new Vector2(-5, 5);
        }
        else
        {
            rb.linearVelocity = new Vector2(5, 5);
        }

        Debug.Log("damage" + currentHealt);

        controlEnemyAnimation.Damage();

        if (currentHealt <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("die");

        controlEnemyAnimation.isLife = false;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        player.gameObject.GetComponent<move_player>().Hill();
    }
}
