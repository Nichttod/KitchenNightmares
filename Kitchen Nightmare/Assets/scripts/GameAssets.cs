using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _i;
        }
    }

    [Header("Item Sprites")]
    public Sprite s_Tomato_Visual;
    public Sprite s_Cabbage_Visual;
    public Sprite s_CheeseBlock_Visual;
    public Sprite s_Bread_Visual;
    public Sprite s_UncookedPattys_Visual;
    [Header("Item ScriptableObjects")]
    public KitchenObjectSO tomatoSO;
    public KitchenObjectSO cabbageSO;
    public KitchenObjectSO cheeseBlockSO;
    public KitchenObjectSO breadSO;
    public KitchenObjectSO uncookedPattysSO;

    

}


