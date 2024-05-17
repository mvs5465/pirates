using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

public class UIController : MonoBehaviour
{
    public static Action<bool, Action> EnableControllerMenu;
    public static Action NotifyRedraw;

    public PanelSettings panelSettings;
    public VisualTreeAsset uiAsset;
    public Sprite healthHeartSprite;
    public Sprite fishSprite;
    public Sprite woodSprite;
    public Sprite hungerSprite;

    private bool shouldDrawNodeMenu = false;
    private Action controllerMenuEvent;
    private GameObject uiContainer;
    private Camera mainCamera;

    private void Awake()
    {
        NotifyRedraw += Draw;
        EnableControllerMenu += ShowControllerMenu;
    }

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        Draw();
    }

    private void ShowControllerMenu(bool shouldDraw, Action clickEvent)
    {
        shouldDrawNodeMenu = shouldDraw;
        controllerMenuEvent = clickEvent;
    }

    private void Draw()
    {
        if (uiContainer) Destroy(uiContainer);
        uiContainer = Utility.AttachChildObject(mainCamera.gameObject, "UIContainer");

        GameObject restartButtonContainer = Utility.AttachChildObject(uiContainer, "UIHealthText");
        UIDocument uiDocument = restartButtonContainer.AddComponent<UIDocument>();
        uiDocument.panelSettings = panelSettings;
        uiDocument.visualTreeAsset = uiAsset;

        DrawHealthWidget(uiDocument);
        DrawFoodWidget(uiDocument);
        DrawInventoryWidget(uiDocument);
        if (shouldDrawNodeMenu) DrawControllerNodeWidget(uiDocument);
    }

    private void DrawHealthWidget(UIDocument uiDocument)
    {
        VisualElement widget = new();
        uiDocument.rootVisualElement.Add(widget);
        widget.style.flexDirection = FlexDirection.Row;
        widget.style.position = Position.Absolute;
        widget.style.top = 0;
        widget.style.left = 0;

        for (int i = 0; i < FindObjectOfType<Player>().GetHealth(); i++)
        {
            Image image = new();
            image.style.height = 15;
            image.style.width = 15;
            image.style.marginTop = image.style.marginLeft = 2;
            image.sprite = healthHeartSprite;
            widget.Add(image);
        }
    }
    private void DrawFoodWidget(UIDocument uiDocument)
    {
        VisualElement widget = new();
        uiDocument.rootVisualElement.Add(widget);
        widget.style.flexDirection = FlexDirection.Row;
        widget.style.position = Position.Absolute;
        widget.style.top = 20;
        widget.style.left = 0;

        for (int i = 0; i < FindObjectOfType<Player>().GetFood(); i++)
        {
            Image image = new();
            image.style.height = 12;
            image.style.width = 12;
            image.style.marginTop = image.style.marginLeft = 2;
            image.sprite = hungerSprite;
            widget.Add(image);
        }
    }

    private void DrawInventoryWidget(UIDocument uiDocument)
    {
        VisualElement widget = new();
        widget.style.flexDirection = FlexDirection.Row;
        widget.style.position = Position.Absolute;
        widget.style.top = 40;
        widget.style.left = 0;
        uiDocument.rootVisualElement.Add(widget);

        foreach (Item item in PlayerInventory.GetItemList())
        {
            Image image = new();
            image.style.height = 12;
            image.style.width = 12;
            image.style.marginTop = image.style.marginLeft = 3;
            image.sprite = item.Sprite;
            widget.Add(image);

            Label name = new();
            name.style.fontSize = 9;
            name.style.color = Color.white;
            name.style.marginTop = name.style.marginLeft = 3;
            name.text = string.Format("{0}", item.ItemName);
            widget.Add(name);
        }
    }

    private void DrawControllerNodeWidget(UIDocument uiDocument)
    {
        VisualElement widget = new();
        uiDocument.rootVisualElement.Add(widget);
        widget.style.flexDirection = FlexDirection.Row;
        widget.style.position = Position.Absolute;
        widget.style.top = 70;
        widget.style.left = 50;

        Button button = new();
        button.style.height = 30;
        button.style.width = 100;
        button.style.fontSize = 9;
        button.style.marginTop = button.style.marginLeft = 3;
        string displayText;
        if (UnityEngine.Random.Range(2, 3) >= 2)
        {
            displayText = "Planter  -  2 Wood";
            button.style.color = Color.white;
            button.clicked += controllerMenuEvent;
        }
        else
        {
            displayText = "Planter  -  2 Wood\n(Cannot Afford)";
            button.style.color = Color.red;
        }
        button.text = displayText;
        button.style.alignItems = Align.FlexStart;
        widget.Add(button);
    }
}
