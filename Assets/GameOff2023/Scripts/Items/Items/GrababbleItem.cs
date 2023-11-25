using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
public class GrababbleItem : Item
{

    [SerializeField]
    private float throwForce;
    private Collider collider;
    private Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void Grab(Transform newParent)
    {
        this.transform.parent = newParent;
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localRotation = Quaternion.identity;

        collider.enabled = false;
        rigidbody.isKinematic = true;

        Debug.Log("Grabbed item");

    }

    virtual public void Drop()
    {
        this.transform.parent = null;
        collider.enabled = true;
        rigidbody.isKinematic = false;

        Debug.Log("Dropped item");
    }

    virtual public void Use(GameObject usingObject)
    {

    }

    virtual public void Throw()
    {
        Drop();
        this.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);

    }
}
