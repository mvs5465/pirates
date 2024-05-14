using Unity.VisualScripting;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public SpriteAnimatorData animatorData;

    private readonly float duration = 10;
    private readonly float updatePeriod = 0.1f;
    private readonly float growthMultiplier = 1.01f;
    private readonly float speed = 1.5f;
    private readonly float resetTime = 3;

    private Rigidbody2D rb;
    public float startX;
    public float startY;
    private float elapsedTime = 0;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        startX = transform.position.x;
        startY = transform.position.y;
        Invoke(nameof(Grow), updatePeriod + Random.Range(0f, resetTime));
    }
    void FixedUpdate()
    {
        if (elapsedTime != 0)
        {
            rb.MovePosition(rb.transform.position + speed * Time.fixedDeltaTime * Vector3.down + Vector3.right * Random.Range(-0.1f, 0.1f));
        }
    }
    void Grow()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        elapsedTime += updatePeriod;
        transform.localScale *= growthMultiplier;
        if (elapsedTime > duration / 2f)
        {
            CancelInvoke();
            Invoke(nameof(Shrink), updatePeriod);
        }
        else
        {
            Invoke(nameof(Grow), updatePeriod);
        }
    }
    void Shrink()
    {
        elapsedTime += updatePeriod;
        transform.localScale *= 2 - growthMultiplier;

        if (elapsedTime > duration)
        {
            elapsedTime = 0;
            rb.MovePosition(new Vector2(startX, startY));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Invoke(nameof(Grow), Random.Range(0f, resetTime));
        }
        else
        {
            Invoke(nameof(Shrink), updatePeriod);
        }
    }
}
