using UnityEngine;

public class AIController
{
    private readonly Rigidbody2D rb;
    private readonly float maxSpeed;
    private int timer;
    private int timerReset;
    private Vector3 moveDirection;

    public AI_Controller(GameObject target, float maxSpeed)
    {
        rb = target.GetComponent<Rigidbody2D>();
        if (!rb) rb = target.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        this.maxSpeed = maxSpeed;

        timer = 0;
        timerReset = Random.Range(0, 30);
        RegenerateMoveDirection();
    }

    public void OnFixedUpdate()
    {
        if (timer < timerReset)
        {
            timer += 1;
            rb.MovePosition(rb.transform.position + maxSpeed * Time.fixedDeltaTime * moveDirection.normalized);
            return;
        }
        else
        {
            timer = 0;
            timerReset = Random.Range(0, 30);
            RegenerateMoveDirection();
            rb.transform.up = moveDirection;
        }
    }

    private void RegenerateMoveDirection()
    {
        moveDirection = Vector3.zero;
        moveDirection += Vector3.left * Random.Range(0, 2);
        moveDirection += Vector3.right * Random.Range(0, 2);
        moveDirection += Vector3.up * Random.Range(0, 2);
        moveDirection += Vector3.down * Random.Range(0, 2);
    }
}