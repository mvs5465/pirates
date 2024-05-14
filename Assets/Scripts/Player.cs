using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action GiveFish;
    public static Action<int> GiveWood;
    public static Action<int> GiveFood;

    private readonly float MaxSpeed = 4;
    private int health = 5;
    private int hunger = 5;
    private int fish = 0;
    private int wood = 0;

    private MovementController movementController;
    private InteractableNode currentInteractable;

    private class InteractableManager : MonoBehaviour
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
    private void Awake()
    {
        Application.targetFrameRate = 60;

        GiveFish += AddFish;
        GiveWood += AddWood;
        GiveFood += AddFood;
    }
    private void Start()
    {
        movementController = new(gameObject, MaxSpeed);
        Utility.AttachChildObject(gameObject, "InteractableManager").AddComponent<InteractableManager>();
        InvokeRepeating(nameof(PassiveEat), 20, 20);
    }
    private void PassiveEat()
    {
        GiveFood(-1);
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetFishCount()
    {
        return fish;
    }
    public int GetWoodCount()
    {
        return wood;
    }
    private void AddFish()
    {
        fish++;
        if (fish < 0) fish = 0;
        UIController.NotifyRedraw();
    }
    private void AddWood(int count)
    {
        wood += count;
        if (wood < 0) wood = 0;
        UIController.NotifyRedraw();
    }
    private void AddFood(int count)
    {
        hunger += count;
        if (hunger < 0) hunger = 0;
        UIController.NotifyRedraw();
    }
    private void FixedUpdate()
    {
        movementController.OnFixedUpdate();
    }
    public int GetHunger()
    {
        return hunger;
    }
}
