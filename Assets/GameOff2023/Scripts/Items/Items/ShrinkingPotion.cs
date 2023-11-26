using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class ShrinkingPotion : GrababbleItem, IUsableItem
{
    //TODO: fail to use sound
    [SerializeField]
    private AudioClip useAudio;
    public void Use(GameObject usingObject)
    {
        ScalableObject scaling = usingObject.GetComponent<ScalableObject>();
        if(scaling == null)
        {
            Debug.LogError("No scalableObject script attached to " + usingObject.name + " Object wont be scaled");
            return;
        }
        else if(scaling != null)
        {
            audioSource.PlayOneShot(useAudio);
            Transform currentParent = this.transform.parent;
            //Is being held by player
            FirstPersonController fpc = GetComponentInParent<FirstPersonController>();
            if (fpc != null && fpc.gameObject == usingObject)
            {
                //Work around for scaling issue, scaling potions are the only thing that can be held while you change size
                this.transform.parent = null;
                this.transform.localScale = new Vector3(1, 1, 1);
                bool wasUsed = scaling.Shrink();
                this.transform.parent = currentParent;
                this.transform.localPosition = new Vector3(0, 0, 0);
                this.transform.localRotation = Quaternion.identity;


                if (wasUsed)
                {
                    Destroy(this.gameObject);
                    //TODO: Do things for if item was in player inventory
                }
            }
            //No need for any work around if not being held by player
            else
            {
                if(scaling.Shrink())
                {
                    //Remove when used
                    Destroy(this);
                }
            }



        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ScalableObject otherObject = collision.gameObject.GetComponent<ScalableObject>();
        if (otherObject == null) return;
        audioSource.PlayOneShot(useAudio);
        if(otherObject.Shrink())
        {
            Destroy(this.gameObject);
        }
    }


}
