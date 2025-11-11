using UnityEngine;

public class GearWheelScript : MonoBehaviour
{
    float speedRotation = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0, speedRotation));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<move_player>())
        {
            collision.GetComponent<move_player>().TakeDamage(1000);
        }
    }
}
