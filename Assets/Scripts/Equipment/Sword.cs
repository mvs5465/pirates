using UnityEngine;

public class Sword : Tool
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
        if (Input.GetKeyDown(KeyCode.E) && !slashing)
        {
            slashing = true;
            spriteRenderer.sprite = slashSprite;
            transform.localScale *= 1.25f;
            gameObject.AddComponent<CircleCollider2D>().isTrigger = true;;
            Invoke(nameof(StopSlashing), 0.2f);
        }
    }

    void StopSlashing()
    {
        spriteRenderer.sprite = swordSprite;
        transform.localScale *= 0.8f;
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Invoke(nameof(ClearCooldown), 0.3f);
    }
    void ClearCooldown() {
        slashing = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Skelly>()) {
            Destroy(other.gameObject);
        }
    }

    public override void Shutdown() {
        Destroy(gameObject);
    }
}
