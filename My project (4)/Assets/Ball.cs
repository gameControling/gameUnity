using UnityEngine;

public class Ball : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string InteractionHintText => "ѕнуть м€ч";

    public void Interact(GameObject interactor)
    {
        Debug.Log("Kick ball!");
        var physicInteractor = interactor.GetComponent<IPhysicInteractor>();
        
        if (physicInteractor != null)
        {
            Vector3 actorDirection = (transform.position - interactor.transform.position).normalized;
            Debug.Log(actorDirection);
            _rigidbody.AddForce(new Vector3(actorDirection.x, 0.5f, actorDirection.z) * physicInteractor.kickPower,
                ForceMode.Impulse);
        }
    }
}
