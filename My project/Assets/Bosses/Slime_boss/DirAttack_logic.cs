using UnityEngine;

public class DirAttack_logic : MonoBehaviour
{
    float timerLife = 0.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
        {
            transform.position += new Vector3(4f, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position -= new Vector3(4f, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerLife > 0) 
        {
            timerLife -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
