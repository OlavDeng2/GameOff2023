using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item, IGrabbableItem, IUsableItem
{
    [SerializeField]
    private float throwForce;
    private Collider collider;
    private Rigidbody rigidbody;


    private void Start()
    {
        collider = this.GetComponent<Collider>();
        rigidbody = this.GetComponent<Rigidbody>();
    }
    public void Grab(Transform newParent)
    {
        this.transform.parent = newParent;
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localRotation = Quaternion.identity;

        collider.enabled = false;
        rigidbody.isKinematic = true;

        Debug.Log("Grabbed item");

    }

    public void Drop()
    {
        this.transform.parent = null;
        collider.enabled = true;
        rigidbody.isKinematic = false;

        Debug.Log("Dropped item");

    }

    public void Use()
    {
        //TODO: Do stuff to use the potion(use drinks)
    }

    public void Throw()
    {
        Drop();
        this.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);

    }
}
