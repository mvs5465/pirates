using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action<int> GiveFood;

    private readonly float MaxSpeed = 4;
    private int health = 5;
    private int hunger = 5;

    private MovementController movementController;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        GiveFood += AddFood;
    }
    private void Start()
    {
        movementController = new(gameObject, MaxSpeed);

        Utility.AttachChildObject(gameObject, "InteractableManager").AddComponent<InteractableManager>();
        Utility.AttachChildObject(gameObject, "PlayerInventory").AddComponent<PlayerInventory>();

        InvokeRepeating(nameof(PassiveEat), 20, 20);
    }
    private void FixedUpdate()
    {
        movementController.OnFixedUpdate();
    }

    private void PassiveEat()
    {
        AddFood(-1);
    }

    ////////////////////////
    // Resource Behaviors //
    public int GetHealth()
    {
        return health;
    }
    private void AddHealth(int amount)
    {
        health += amount;
        if (health < 0) health = 0;
        if (health > 5) health = 5;
        UIController.NotifyRedraw();
    }

    public int GetFood()
    {
        return hunger;
    }
    private void AddFood(int count)
    {
        hunger += count;
        if (hunger < 0) hunger = 0;
        UIController.NotifyRedraw();
    }
}
