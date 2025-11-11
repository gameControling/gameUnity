using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private Ball ball;
    [SerializeField] Vector3 position;
    Transform rotate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotate = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string InteractionHintText => "Получить мяч";

    public void Interact(GameObject interactor)
    {
        Debug.Log("Klick button");
        position.y = interactor.transform.position.y + 10;
        Instantiate(ball, position, rotate.rotation);
    }
}
