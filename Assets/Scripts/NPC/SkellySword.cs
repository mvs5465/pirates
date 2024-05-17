using UnityEngine;

public class SkellySword : MonoBehaviour
{

    public Sprite swordSprite;
    public Sprite slashSprite;

    private SpriteRenderer spriteRenderer;
    private bool slashing = false;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (!spriteRenderer) gameObject.AddComponent<SpriteRenderer>();

        spriteRenderer.sprite = swordSprite;
    }

    void Update()
    {
        if (!slashing)
        {
            slashing = true;
            spriteRenderer.sprite = slashSprite;
            transform.localScale *= 2.5f;
            gameObject.AddComponent<CircleCollider2D>().isTrigger = true;;
            Invoke(nameof(StopSlashing), 0.2f);
        }
    }

    void StopSlashing()
    {
        spriteRenderer.sprite = swordSprite;
        transform.localScale *= 0.4f;
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Invoke(nameof(ClearCooldown), 0.3f);
    }
    void ClearCooldown() {
        slashing = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Player>()) {
            Debug.Log("Got the player!");
        }
    }
}
