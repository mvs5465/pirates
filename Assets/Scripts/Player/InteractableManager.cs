using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private InteractableNode currentInteractable;
    private void Start()
    {
        CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = 2;
        circleCollider2D.isTrigger = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable)
        {
            currentInteractable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        InteractableNode interactableNode = other.GetComponent<InteractableNode>();
        if (interactableNode)
        {
            currentInteractable = interactableNode;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        InteractableNode interactableNode = other.GetComponent<InteractableNode>();
        if (interactableNode && interactableNode == currentInteractable)
        {
            currentInteractable.Cancel();
            currentInteractable = null;
        }
    }
}