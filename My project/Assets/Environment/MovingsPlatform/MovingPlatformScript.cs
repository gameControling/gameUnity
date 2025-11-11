using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] GameObject pointRight, pointLeft, triggerStart;
    Rigidbody2D rb;
    float speed = 10;
    [SerializeField] Vector3 target, onTarget;
    bool rightPos = false;
    [SerializeField] float timer = 0;
    bool userActTrig = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointLeft.transform.position;
        triggerStart.GetComponent<TriggersActivate>().OnDisTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Move();
        }
        else
        {
            timer -= Time.deltaTime;
            transform.position = onTarget;
        }
    }

    private void Move()
    {

        if (triggerStart.GetComponent<TriggersActivate>().TriggerActivate())
        { 
            userActTrig = true;
        }
        if(userActTrig)
        { 
            if (transform.position != target)
            {
                Vector3 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else
            {
                onTarget = target;
                if (rightPos)
                {
                    target = pointLeft.transform.position;
                    rightPos = false;
                }
                else
                {
                    target = pointRight.transform.position;
                    rightPos = true;
                }
                timer = 5f;
                userActTrig = false;
            }
        }
        else if (transform.position == onTarget)
        {
            transform.position = onTarget;
        }
    }
}
