using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;
 

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // No object on counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something               
                
                if (HasRecipeWithInput( player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;


                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
                else
                {
                    // Player is not carrying aynthing
                }
            }
        }
        else
        {
            // There is already a KitchenObject on the counter
            if (!player.HasKitchenObject())
            {
                // Player takes the object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            // else: both carrying something → do nothing
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // There is a KitchenObject here and it can be cut        
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObject inputObject = GetKitchenObject();
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // Destroy old object before spawning new
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
          return  cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }                   
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
         foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
    
}
