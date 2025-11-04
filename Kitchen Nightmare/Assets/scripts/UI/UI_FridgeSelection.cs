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
    
    foreach (Transform child in container)
    {
        if (child != itemTemplate)
            Destroy(child.gameObject);
    }    

    int index = 0;
    float itemHeight = 100f; 

    foreach (var kvp in storedItems)
    {
        KitchenObjectSO itemSO = kvp.Key;
        int amount = kvp.Value;

        Transform t = Instantiate(itemTemplate, container);
        t.gameObject.SetActive(true);
      
        RectTransform itemRect = t.GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(0, -itemHeight * index);
     
        t.Find("background/nameText").GetComponent<TextMeshProUGUI>().text = $"{itemSO.name} x{amount}";
        t.Find("background/icon").GetComponent<Image>().sprite = itemSO.sprite;

       
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

        index++; 
    }
}



       
 
    

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
