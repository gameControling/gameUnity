using UnityEngine;

public class DeleteBoom : MonoBehaviour
{
    float timeDelete = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelete > 0) 
        { 
            timeDelete -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
