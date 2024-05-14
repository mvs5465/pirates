using UnityEngine;

public class MovementController
{
    private readonly Rigidbody2D rb;
    private readonly float maxSpeed;
    public MovementController(GameObject target, float maxSpeed)
    {
        rb = target.GetComponent<Rigidbody2D>();
        if (!rb) rb = target.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        this.maxSpeed = maxSpeed;
    }
    public void OnFixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;
        if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.up;
        if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.down;

        if (moveDirection != Vector3.zero) rb.transform.up = -moveDirection;

        rb.MovePosition(rb.transform.position + maxSpeed * Time.fixedDeltaTime * moveDirection.normalized);
    }
}