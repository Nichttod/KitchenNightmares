using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private float shopItemHeight = 100f;
    private Transform shopItemTemplate;
    private Transform container;
    private ShopCounter currentShopCounter; 

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(ItemShop.GetSprite(ItemShop.ItemType.Tomato), "Tomato", ItemShop.GetCost(ItemShop.ItemType.Tomato), 0);
        CreateItemButton(ItemShop.GetSprite(ItemShop.ItemType.CheeseBlock), "CheeseBlock", ItemShop.GetCost(ItemShop.ItemType.CheeseBlock), 1);
        CreateItemButton(ItemShop.GetSprite(ItemShop.ItemType.Cabbage), "Cabbage", ItemShop.GetCost(ItemShop.ItemType.Cabbage), 2);
        CreateItemButton(ItemShop.GetSprite(ItemShop.ItemType.Bread), "Bread", ItemShop.GetCost(ItemShop.ItemType.Bread), 3);
        CreateItemButton(ItemShop.GetSprite(ItemShop.ItemType.UncookedPattys), "UncookedPattys", ItemShop.GetCost(ItemShop.ItemType.UncookedPattys), 4);
    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);

        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("background/nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("background/priceText").GetComponent<TextMeshProUGUI>().SetText($"{itemCost}$");
        shopItemTransform.Find("background/itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Player player = Player.Instance;

                        if (!player.HasKitchenObject())
            {
                int itemCost = ItemShop.GetCost(GetItemTypeByName(itemName));

                //  Check if player has enough money
                if (CurrencyManager.Instance.TrySpendMoney(itemCost))
                {
                    // Successful purchase
                    KitchenObjectSO kitchenObjectSO = ItemShop.GetKitchenObjectSO(GetItemTypeByName(itemName));
                    KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                    Debug.Log($"Bought {itemName} for ${itemCost}");

                    // Close shop
                    if (currentShopCounter != null)
                    {
                        currentShopCounter.CloseShop();
                        currentShopCounter = null;
                    }
                }
                else
                {
                    Debug.Log("❌ Not enough money to buy " + itemName);
                }
            }            
        };
    }

    private ItemShop.ItemType GetItemTypeByName(string itemName)
    {
        return (ItemShop.ItemType)System.Enum.Parse(typeof(ItemShop.ItemType), itemName);
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void SetShopCounter(ShopCounter shopCounter) 
    {
        currentShopCounter = shopCounter;
    }
}
