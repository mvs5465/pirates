using UnityEngine;

public abstract class InteractableNode : MonoBehaviour {
    public virtual void Interact() {}
    public virtual void Cancel() {}
}