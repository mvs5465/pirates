using System;
using UnityEngine;

public class ShipNode : InteractableNode
{
    public GameObject planterNodePrefab;
    public static Action BuyPlanterNode;

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
        // TODO: remove items from player inventory
        Destroy(gameObject);
        CancelInvoke();
    }

    public override void Interact()
    {
        UIController.EnableControllerMenu(true);
        UIController.NotifyR edraw();
    

    public override void Cancel()
    {
        UIController.EnableControllerMenu(false);
        UIController.NotifyRedraw();
    }
}