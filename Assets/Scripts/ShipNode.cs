using System;
using UnityEngine;

public class ShipNode : InteractableNode
{
    public GameObject planterNodePrefab;
    private Action BuyPlanterNode;

    void Awake()
    {
        BuyPlanterNode += BecomePlanterNode;
    }

    void Start()
    {
        BoxCollider2D circleCollider2D = gameObject.AddComponent<BoxCollider2D>();
        circleCollider2D.isTrigger = true;
    }

    private void BecomePlanterNode()
    {
        GameObject planterNodeObj = Instantiate(planterNodePrefab, transform.position, Quaternion.identity);
        planterNodeObj.transform.parent = transform.parent;
        Player.GiveWood(-2);
        Destroy(gameObject);
        CancelInvoke();
    }

    public override void Interact()
    {
        UIController.EnableControllerMenu(true, BuyPlanterNode);
        UIController.NotifyRedraw();
    }

    public override void Cancel()
    {
        UIController.EnableControllerMenu(false, null);
        UIController.NotifyRedraw();
    }
}