using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnlargingPotion : GrababbleItem, IUsableItem
{

    public override void Use(GameObject usingObject)
    {
        base.Use(usingObject);
        ScalableObject scaling = usingObject.GetComponent<ScalableObject>();
        if(scaling == null)
        {
            Debug.LogError("No scalableObject script attached to " + usingObject.name + " Object wont be scaled");
            return;
        }
        else if(scaling != null)
        {
            Transform currentParent = this.transform.parent;
            //Is being held by player
            FirstPersonController fpc = GetComponentInParent<FirstPersonController>();
            if (fpc != null && fpc.gameObject == usingObject)
            {
                //Work around for scaling issue, scaling potions are the only thing that can be held while you change size
                this.transform.parent = null;
                this.transform.localScale = new Vector3(1, 1, 1);
                scaling.Grow();
                this.transform.parent = currentParent;
                this.transform.localPosition = new Vector3(0, 0, 0);
                this.transform.localRotation = Quaternion.identity;
            }
            //No need for any work around if not being held by player
            else
            {
                scaling.Grow();
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ScalableObject otherObject = collision.gameObject.GetComponent<ScalableObject>();
        if (otherObject == null) return;

        otherObject.Grow();
        Destroy(this.gameObject);
    }


}
