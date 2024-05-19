using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanterNode : InteractableNode
{
    public static Func<bool> CurrentlyActive;
    public List<Sprite> frames;
    private readonly int phaseTime = 10;

    private void Awake()
    {
        Invoke(nameof(Grow), phaseTime);
    }

    private void Start()
    {
        BoxCollider2D circleCollider2D = gameObject.AddComponent<BoxCollider2D>();
        circleCollider2D.isTrigger = true;
    }

    public override void Interact()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == frames.Last())
        {
            Player.GiveFood(4);
            gameObject.GetComponent<SpriteRenderer>().sprite = frames.First();
        }
    }

    private void Grow()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == frames.Last())
        {
            CancelInvoke();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = frames[frames.IndexOf(gameObject.GetComponent<SpriteRenderer>().sprite) + 1];
            Invoke(nameof(Grow), phaseTime);
        }
    }
}