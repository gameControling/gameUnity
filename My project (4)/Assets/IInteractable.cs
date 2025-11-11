using UnityEngine;

public interface IInteractable
{
    public string InteractionHintText { get; }
    void Interact(GameObject interactor);
}
