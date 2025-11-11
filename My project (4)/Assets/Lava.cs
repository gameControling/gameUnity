using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("trigger");
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>()?.TakeDamage(damage);
        }
    }
}
