using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FishingRod : Tool
{
    public GameObject startNodeReference;
    public GameObject bobber;
    private bool isCast = false;
    private bool bobbing = false;
    private LineRenderer lr;
    private GameObject deployedBobber;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Cast();
        }

        if (isCast)
        {
            lr.SetPosition(0, startNodeReference.transform.position);
        }
    }

    void Cast()
    {
        if (!isCast)
        {
            Vector3 castTarget = startNodeReference.transform.position + transform.parent.up * 4;
            if (lr == null) lr = gameObject.AddComponent<LineRenderer>();
            lr.SetPosition(0, startNodeReference.transform.position);
            lr.SetPosition(1, castTarget);
            lr.material = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
            lr.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            lr.startColor = Color.yellow / 4f + Color.red / 4f + Color.white / 3f;
            lr.endColor = Color.yellow / 3f + Color.red / 3f + Color.white / 3f;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.positionCount = 2;
            isCast = true;

            deployedBobber = Instantiate(bobber, castTarget, Quaternion.identity);
            Invoke(nameof(BobUp), Random.Range(3, 8));
        }
        else
        {
            if (bobbing)
            {
                PlayerInventory.GiveItem(FishingLootManager.GetRandomItem());
            }
            CancelInvoke();
            Destroy(deployedBobber);
            Destroy(gameObject.GetComponent<LineRenderer>());
            isCast = false;
            bobbing = false;
        }
    }

    private void BobUp()
    {
        if (!isCast) return;
        bobbing = true;
        deployedBobber.transform.localScale *= 1.25f;
        Invoke(nameof(BobDown), 0.5f);
        deployedBobber.AddComponent<Light2D>().lightType = Light2D.LightType.Point;
    }

    private void BobDown()
    {
        if (!isCast) return;
        deployedBobber.transform.localScale *= 0.4f;
        Invoke(nameof(BobReset), 0.5f);
    }

    private void BobReset()
    {
        if (!isCast) return;
        bobbing = false;
        deployedBobber.transform.localScale *= 2;
        Destroy(deployedBobber.GetComponent<Light2D>());
    }

    public override void Shutdown()
    {
        CancelInvoke();
        Destroy(deployedBobber);
        Destroy(gameObject);
    }
}