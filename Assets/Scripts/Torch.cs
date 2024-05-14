using UnityEngine;

public class Torch : Tool
{
    public SpriteAnimatorData animatorData;
    void Start()
    {
        SpriteAnimator.Build(gameObject, animatorData, "Default", 15);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.parent = null;
        }
    }
}