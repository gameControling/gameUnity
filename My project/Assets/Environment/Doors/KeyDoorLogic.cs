using UnityEngine;

public class KeyDoorLogic : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform door;
    float timerOnDown;
    float timeDown = 1;
    float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((timerOnDown += Time.deltaTime) <= timeDown)
        {
            rb.transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            speed = -speed;
            timerOnDown = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            door.GetComponent<DoorLogic>().OpenDoor();
            Destroy(gameObject);
        }
    }
}
