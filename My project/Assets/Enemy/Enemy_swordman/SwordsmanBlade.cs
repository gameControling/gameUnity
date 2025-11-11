using UnityEngine;

public class SwordsmanBlade : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject knight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if(knight.GetComponent<SpriteRenderer>().flipX)
        //{
        //    transform.position = new Vector3(knight.transform.position.x + 0.69f, knight.transform.position.y);
        //}
        //else
        //{
        //    transform.position = new Vector3(knight.transform.position.x - 0.69f, knight.transform.position.y);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<move_player>())
        {
            collision.GetComponent<move_player>().TakeDamage(damage);
        }
    }
}
