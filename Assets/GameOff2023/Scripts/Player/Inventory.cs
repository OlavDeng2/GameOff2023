using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject owningPlayer;
    private ScalableObject playerScale;
    [SerializeField]
    private InputActionReference grabAction, useAction, throwAction;

    [SerializeField]
    private Transform itemPosition;

    private GrababbleItem heldItem = null;
    private Item currentlyLookingAt;
    [SerializeField]
    private float grabDistance = 1.0f; //Should grab distance change based on size?
    [SerializeField]
    private LayerMask grabLayers;


    // Start is called before the first frame update
    void OnEnable()
    {
        grabAction.action.performed += GrabItem;
        useAction.action.performed += UseItem;
        throwAction.action.performed += ThrowItem;
    }

    private void OnDisable()
    {
        grabAction.action.performed -= GrabItem;
        useAction.action.performed -= UseItem;
        throwAction.action.performed -= ThrowItem;

    }

    private void Start()
    {
        playerScale = owningPlayer.GetComponent<ScalableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Raycast out if item
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance, grabLayers))
        {
            currentlyLookingAt = hit.transform.gameObject.GetComponent<Item>();

            if (currentlyLookingAt != null)
            {
                //if Item, check if grabable or usable.
                if(currentlyLookingAt as GrababbleItem != null)
                {
                    //Is Grabable
                    //TODO: Show UI that you can grab item
                }
                if(currentlyLookingAt as IUsableItem != null)
                {
                    //Is Usable
                    //TODO: Show UI that you can use item
                }
                
            }
        }

    }

    public void UseItem(InputAction.CallbackContext obj)
    {
        IUsableItem usableItem;
        if (heldItem != null)
        {
            usableItem = heldItem as IUsableItem;
            if(heldItem as IUsableItem != null)
            {
                usableItem.Use(owningPlayer);
            }
        }

        else
        {
            usableItem = currentlyLookingAt as IUsableItem;
            if(usableItem != null)
            {
                usableItem.Use(owningPlayer);
            }


        }
    }

    public void GrabItem(InputAction.CallbackContext obj)
    {
        if (heldItem != null)
        {
            heldItem.Drop();
            heldItem = null;
        }

        //TODO: Unnest these if statements a bit, its very messy
        else if(heldItem == null)
        {
            if(currentlyLookingAt is GrababbleItem)
            {
                //Grab item currently being raycasted at
                GrababbleItem grabbingItem = currentlyLookingAt as GrababbleItem;

                //Get current scale of grabable item. Get current scale of player
                //Check if player is allowed to grab item at that scale
                ScalableObject itemScaler = grabbingItem.GetComponent<ScalableObject>();
                if(itemScaler != null)
                {
                    switch (itemScaler.currentScale)
                    {
                        case ScalableObject.Scale.Small:
                            if ((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldSmall)
                            {
                                grabbingItem.Grab(itemPosition);
                                heldItem = grabbingItem;
                            }
                            break;
                        case ScalableObject.Scale.Medium:
                            if ((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldMedium)
                            {
                                grabbingItem.Grab(itemPosition);
                                heldItem = grabbingItem;
                            }
                            break;
                        case ScalableObject.Scale.Big:
                            if ((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldBig)
                            {
                                grabbingItem.Grab(itemPosition);
                                heldItem = grabbingItem;
                            }
                            break;
                    }
                }


                //If no itemscaler, we will base it of playerSizeToHoldMedium TODO: Clean up the playersizetohold stuff
                if(itemScaler == null)
                {
                    if((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldMedium)
                    {
                        grabbingItem.Grab(itemPosition);
                        heldItem = grabbingItem;
                    }
                }

            }
        }
    }

    public void ThrowItem(InputAction.CallbackContext obj)
    {
        if (heldItem == null) return;

        heldItem.Throw();
        heldItem = null;
    }
}
