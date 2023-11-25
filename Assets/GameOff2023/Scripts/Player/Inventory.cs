using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject owningPlayer;
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

        else if(heldItem == null)
        {
            if(currentlyLookingAt is GrababbleItem)
            {
                //Grab item currently being raycasted at
                GrababbleItem grabbingItem = currentlyLookingAt as GrababbleItem;
                grabbingItem.Grab(itemPosition);
                heldItem = grabbingItem;
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
