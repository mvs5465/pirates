using UnityEngine;

public class Skelly : MonoBehaviour
{
    private AIController aiController;
    private readonly float MaxSpeed = 3;

    void Start()
    {
        aiController = new(gameObject, MaxSpeed);
    }

    void FixedUpdate() {
        aiController.OnFixedUpdate();
    }
}