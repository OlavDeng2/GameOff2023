using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Teleporter : Item, IUsableItem
{
    [SerializeField]
    private Transform teleportPoint;

    //TODO: Sound for using item
    public void Use(GameObject usingObject)
    {
        Debug.Log(usingObject.name);
        if(teleportPoint == null)
        {
            Debug.Log("No teleport point set in Teleporter attached to " + this.gameObject.name);
            return;
        }
        usingObject.GetComponent<CharacterController>().enabled = false;
        usingObject.transform.localPosition = teleportPoint.position;
        usingObject.GetComponent<CharacterController>().enabled = true;

        Debug.Log("Teleporting player");
    }

}
