using System.Collections.Generic;
using UnityEngine;

public class FridgeCounter : BaseCounter
{
    [SerializeField] private UI_FridgeStatus fridgeStatusUI; // Small always-visible UI
    [SerializeField] private UI_FridgeSelection fridgeSelectionUI; // Popup selection UI
    [SerializeField] private int maxUniqueItems = 3;
    [SerializeField] private int maxStackPerItem = 15;

    private Dictionary<KitchenObjectSO, int> storedItems = new Dictionary<KitchenObjectSO, int>();

    public override void Interact(Player player)
    {
        
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            KitchenObjectSO objSO = kitchenObject.GetKitchenObjectSO();
            
            if (storedItems.ContainsKey(objSO))
            {
                if (storedItems[objSO] < maxStackPerItem)
                {
                    storedItems[objSO]++;
                    kitchenObject.DestroySelf();
                }

            }
            else if (storedItems.Count < maxUniqueItems)
            {
                storedItems.Add(objSO, 1);
                kitchenObject.DestroySelf();
            }
            fridgeStatusUI.UpdateDisplay(storedItems);
        }        
        else
        {
            if (storedItems.Count > 0)
            {
               
                fridgeSelectionUI.Show(this, storedItems);
            }

        }
    }

    public void TakeOutItem(KitchenObjectSO itemSO, Player player)
    {
            if (player.HasKitchenObject())             
                 return;
        if (storedItems.ContainsKey(itemSO))
        {        
            KitchenObject.SpawnKitchenObject(itemSO, player);
            storedItems[itemSO]--;
                        
            if (storedItems[itemSO] <= 0)
                storedItems.Remove(itemSO);
                
                fridgeSelectionUI.Show(this, storedItems);
                fridgeStatusUI.UpdateDisplay(storedItems);
                
                if (storedItems.Count == 0)
                {
                    fridgeSelectionUI.Hide();
                }        
    }
}
    public void CloseFridgeUI()
    {
        if (fridgeSelectionUI != null)
        {
            fridgeSelectionUI.Hide();
        }
    }
}


