using UnityEngine;

public class TriggersActivate : MonoBehaviour
{
    bool triggerActivate = false;
    bool disactivateTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ActivTrig");
        if (collision.gameObject.GetComponent<move_player>())
        {
            triggerActivate = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ExitTrig");
        if (disactivateTrigger && collision.gameObject.GetComponent<move_player>())
        {
            triggerActivate = false;
        }
    }

    public bool TriggerActivate() { return triggerActivate; }

    public void OnDisTrigger()
    {
        disactivateTrigger = true;
    }
}
