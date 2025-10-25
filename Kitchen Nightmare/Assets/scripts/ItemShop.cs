using UnityEngine;

public class ItemShop 
{
    public enum ItemType
    {
        Tomato,
        Cabbage,
        CheeseBlock,
        Bread,
        UncookedPattys,
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Tomato: return 3;
            case ItemType.Cabbage: return 4;            
            case ItemType.CheeseBlock: return 3;            
            case ItemType.Bread: return 2;
            case ItemType.UncookedPattys: return 5;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Tomato: return GameAssets.i.s_Tomato_Visual;
            case ItemType.Cabbage: return GameAssets.i.s_Cabbage_Visual;
            case ItemType.CheeseBlock: return GameAssets.i.s_CheeseBlock_Visual;
            case ItemType.Bread: return GameAssets.i.s_Bread_Visual;
            case ItemType.UncookedPattys: return GameAssets.i.s_UncookedPattys_Visual;
        }
    }
    public static KitchenObjectSO GetKitchenObjectSO(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Tomato: return GameAssets.i.tomatoSO;
            case ItemType.Cabbage: return GameAssets.i.cabbageSO;
            case ItemType.CheeseBlock: return GameAssets.i.cheeseBlockSO;
            case ItemType.Bread: return GameAssets.i.breadSO;
            case ItemType.UncookedPattys: return GameAssets.i.uncookedPattysSO;
        }
    }
 

}
