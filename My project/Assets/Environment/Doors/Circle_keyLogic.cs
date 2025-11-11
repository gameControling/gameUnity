using UnityEngine;

public class Circle_keyLogic : MonoBehaviour
{
    [SerializeField] GameObject triggerOne;
    [SerializeField] GameObject triggerTwo;
    [SerializeField] GameObject triggerThree;
    [SerializeField] GameObject Door;
    [SerializeField] GameObject LightOne;
    [SerializeField] GameObject LightTwo;
    [SerializeField] GameObject LightThree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightOne.SetActive(false);
        LightTwo.SetActive(false);
        LightThree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerOne.GetComponent<TriggersActivate>().TriggerActivate())
        {
            LightOne.SetActive(true);
            triggerOne.SetActive(false);
        }
        if (triggerTwo.GetComponent<TriggersActivate>().TriggerActivate())
        {
            LightTwo.SetActive(true);
            triggerTwo.SetActive(false);
        }
        if (triggerThree.GetComponent<TriggersActivate>().TriggerActivate())
        {
            LightThree.SetActive(true);
            triggerThree.SetActive(false);
        }

        if(triggerOne.GetComponent<TriggersActivate>().TriggerActivate() &&
            triggerTwo.GetComponent<TriggersActivate>().TriggerActivate() &&
            triggerThree.GetComponent<TriggersActivate>().TriggerActivate())
        {
            Door.transform.position = new Vector3(156.604f, 109.2419f, 0);
        }
    }
}
