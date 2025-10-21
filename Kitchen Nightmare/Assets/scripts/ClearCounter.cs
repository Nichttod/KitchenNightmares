using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    
    
    private KitchenObject kitchenObject;

   
    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kichtenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kichtenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);        
        }
        else
        {
            //Give the object to the player
           kitchenObject.SetKitchenObjectParent(player);  
            
        }
    }
    public Transform GetKitchenObjectFollowTransfrom()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}