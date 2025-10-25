using UnityEngine;

public class ShopCounter : BaseCounter
{
    [SerializeField] private UI_Shop uiShop; // Reference to your UI prefab or object

    private bool isShopOpen = false;

        public override void Interact(Player player)
        {

        if (isShopOpen)
            CloseShop();

        else
            OpenShop();
        }

        public void OpenShop()
        {
            uiShop.SetShopCounter(this);
            uiShop.Show();
            isShopOpen = true;
        }


        public void CloseShop()
        {
            uiShop.Hide();
            isShopOpen = false;
        }
    }
