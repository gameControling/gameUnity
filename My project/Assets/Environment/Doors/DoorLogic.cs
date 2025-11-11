using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject caslePlatform;

    public void OpenDoor()
    {
        Debug.Log("door open");
        Instantiate(caslePlatform, gameObject.transform.position + new Vector3(-20, -5), caslePlatform.transform.rotation);
        Instantiate(caslePlatform, gameObject.transform.position + new Vector3(-25, -7.5f), caslePlatform.transform.rotation);
        Instantiate(caslePlatform, gameObject.transform.position + new Vector3(-15, -2.5f), caslePlatform.transform.rotation);
        Destroy(gameObject);
        sr.color = new Color(125,125,125,175);
    }
}
