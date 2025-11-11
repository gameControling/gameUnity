using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss_logic : MonoBehaviour
{
    public Transform player;
    public bool isFlip = false;

    public int maxHealth = 500;
    int currentHealt;

    ControlBossAnimation controlBossAnimation;
    Vector3 originalPosition;

    private void Start()
    {
        currentHealt = maxHealth;
        controlBossAnimation = GetComponent<ControlBossAnimation>();
        originalPosition = player.position;
    }

    public void lookAtPlayer()
    {
        if(transform.position.x > player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(transform.position.x < player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        
        Debug.Log("damage" + currentHealt);


        if (currentHealt <= 0)
        {
            Die();
        }
    }

    IEnumerator ShakeCamera()
    {
        float timeLeft = Time.time, x, y;

        while ((timeLeft + 0.8f) > Time.time)
        {
            x = Random.Range(-0.3f, 0.3f);
            y = Random.Range(-0.3f, 0.3f);

            transform.position = new Vector3(x, y, originalPosition.z); yield return new WaitForSeconds(0.025f);
        }

        transform.position = originalPosition;
    }

    private void Die()
    {
        Debug.Log("die");

        controlBossAnimation.isLife = false;
        controlBossAnimation.Damage();

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
