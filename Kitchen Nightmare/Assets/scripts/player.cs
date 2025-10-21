using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private float movespeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking;
    private Vector3 lastInteractionsDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }        
    }   
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()

    {   
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractionsDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionsDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);                
                }
            }
            else
            {
                 SetSelectedCounter(null);          
            }
        }
        else
        {
             SetSelectedCounter(null);           
        }
              Debug.DrawRay(transform.position, lastInteractionsDir * interactDistance, Color.red);   
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movespeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {//Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can move only on the x 
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only one the x
                //Attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move only on the z 
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * movespeed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;
        float rotationspeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationspeed);
    }
        private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs
        {
          selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransfrom()
    {
        return kitchenObjectHoldPoint;
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