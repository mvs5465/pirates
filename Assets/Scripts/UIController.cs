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
        DrawFishWidget(uiDocument);
        DrawWoodWidget(uiDocument);
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

        for (int i = 0; i < FindObjectOfType<Player>().GetHunger(); i++)
        {
            Image image = new();
            image.style.height = 12;
            image.style.width = 12;
            image.style.marginTop = image.style.marginLeft = 2;
            image.sprite = hungerSprite;
            widget.Add(image);
        }
    }

    private void DrawFishWidget(UIDocument uiDocument)
    {
        VisualElement fishWidget = new();
        uiDocument.rootVisualElement.Add(fishWidget);
        fishWidget.style.flexDirection = FlexDirection.Row;
        fishWidget.style.position = Position.Absolute;
        fishWidget.style.top = 40;
        fishWidget.style.left = 0;

        Image fishImage = new();
        fishImage.style.height = 12;
        fishImage.style.width = 12;
        fishImage.style.marginTop = fishImage.style.marginLeft = 3;
        fishImage.sprite = fishSprite;
        fishWidget.Add(fishImage);

        Label fishCount = new();
        fishCount.text = string.Format("{0}", FindObjectOfType<Player>().GetFishCount());
        fishCount.style.fontSize = 9;
        fishCount.style.color = Color.white;
        fishCount.style.marginTop = fishImage.style.marginLeft = 3;
        fishWidget.Add(fishCount);
    }

    private void DrawWoodWidget(UIDocument uiDocument)
    {
        VisualElement woodWidget = new();
        uiDocument.rootVisualElement.Add(woodWidget);
        woodWidget.style.flexDirection = FlexDirection.Row;
        woodWidget.style.position = Position.Absolute;
        woodWidget.style.top = 55;
        woodWidget.style.left = 0;

        Image woodImage = new();
        woodImage.style.height = 12;
        woodImage.style.width = 12;
        woodImage.style.marginTop = woodImage.style.marginLeft = 3;
        woodImage.sprite = woodSprite;
        woodWidget.Add(woodImage);

        Label woodCount = new();
        woodCount.text = string.Format("{0}", FindObjectOfType<Player>().GetWoodCount());
        woodCount.style.fontSize = 9;
        woodCount.style.color = Color.white;
        woodCount.style.marginTop = woodImage.style.marginLeft = 3;
        woodWidget.Add(woodCount);
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
        if (FindObjectOfType<Player>().GetWoodCount() >= 2)
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
