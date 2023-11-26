using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject owningPlayer;
    private ScalableObject playerScale;
    [Header("UI")]
    [SerializeField]
    private GameObject useItemImage;
    [SerializeField]
    private GameObject throwItemImage;
    [SerializeField]
    private GameObject grabItemImage;

    //TODO: Move the audio to the correct place (use, throw, grab and drop should be done in the Item)
    //Fail grab sound will remain part of the inventory
    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip failGrabSound;




    [Header("Input")]
    [SerializeField]
    private InputActionReference grabAction, useAction, throwAction;


    [Header("Settings")]
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
        grabItemImage.SetActive(false);
        useItemImage.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //No point in looking for items or to show UI to grab/use items if there is already a held item
        if (heldItem != null) return;

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
                    grabItemImage.SetActive(true);
                }
                else
                {
                    grabItemImage.SetActive(false);
                }
                if (currentlyLookingAt as IUsableItem != null)
                {
                    //Is Usable
                    useItemImage.SetActive(true);
                }
                else
                {
                    useItemImage.SetActive(false);
                }
                
            }

            
        }
        else
        {
            useItemImage.SetActive(false);
            grabItemImage.SetActive(false);
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

            throwItemImage.SetActive(false);
            useItemImage.SetActive(false);

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
                                //Hide grab item UI
                                grabItemImage.SetActive(false);
                                //Show UI that you can throw item
                                throwItemImage.SetActive(true);
                                //show UI that you can use item
                                if (grabbingItem as IUsableItem != null)
                                {
                                    useItemImage.SetActive(true);
                                }

                            }
                            break;
                        case ScalableObject.Scale.Medium:
                            if ((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldMedium)
                            {
                                grabbingItem.Grab(itemPosition);
                                heldItem = grabbingItem;
                                //Hide grab item UI
                                grabItemImage.SetActive(false);
                                //Show UI that you can throw item
                                throwItemImage.SetActive(true);
                                //show UI that you can use item
                                if (grabbingItem as IUsableItem != null)
                                {
                                    useItemImage.SetActive(true);
                                }

                            }
                            break;
                        case ScalableObject.Scale.Big:
                            if ((int)playerScale.currentScale >= (int)grabbingItem.playerSizeToHoldBig)
                            {
                                grabbingItem.Grab(itemPosition);
                                heldItem = grabbingItem;
                                //Hide grab item UI
                                grabItemImage.SetActive(false);
                                //Show UI that you can throw item
                                throwItemImage.SetActive(true);
                                //show UI that you can use item
                                if (grabbingItem as IUsableItem != null)
                                {
                                    useItemImage.SetActive(true);
                                }

                            }
                            break;
                        default:
                            audioSource.PlayOneShot(failGrabSound);
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
                        grabItemImage.SetActive(false);
                        //Show UI that you can throw item
                        throwItemImage.SetActive(true);
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

        throwItemImage.SetActive(false);
        useItemImage.SetActive(false);

    }
}
