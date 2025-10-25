using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_FridgeSelection : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform itemTemplate;

    private FridgeCounter currentFridge;

    private void Awake()
    {
        itemTemplate.gameObject.SetActive(false);
        Hide();
    }
 
    

   public void Show(FridgeCounter fridge, Dictionary<KitchenObjectSO, int> storedItems)
{
    currentFridge = fridge;
    gameObject.SetActive(true);

    // 🔹 STEP 2 — Clear previous item clones before creating new ones
    foreach (Transform child in container)
    {
        if (child != itemTemplate)
            Destroy(child.gameObject);
    }

    // Optional: make sure canvas is interactive
    CanvasGroup cg = GetComponent<CanvasGroup>();
    if (cg != null)
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    // ✅ Rebuild layout
    LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());

    // ✅ create buttons for each stored item
    int index = 0;
    float itemHeight = 100f; // adjust to your prefab height

    foreach (var kvp in storedItems)
    {
        KitchenObjectSO itemSO = kvp.Key;
        int amount = kvp.Value;

        Transform t = Instantiate(itemTemplate, container);
        t.gameObject.SetActive(true);

        // 🔽 position each item lower based on index
        RectTransform itemRect = t.GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(0, -itemHeight * index);

        // Set text and icon
        t.Find("background/nameText").GetComponent<TextMeshProUGUI>().text = $"{itemSO.name} x{amount}";
        t.Find("background/icon").GetComponent<Image>().sprite = itemSO.sprite;

        // Button logic
        var buttonUI = t.GetComponent<CodeMonkey.Utils.Button_UI>();
        if (buttonUI != null)
        {
                buttonUI.ClickFunc = () =>
            {
                Player player = Player.Instance;
                currentFridge.TakeOutItem(itemSO, player);
            };

        }
        else
        {
            var button = t.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    Player player = Player.Instance;
                    currentFridge.TakeOutItem(itemSO, player);
                });
            }
        }

        index++; // move to next line
    }
}



       
 
    

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
