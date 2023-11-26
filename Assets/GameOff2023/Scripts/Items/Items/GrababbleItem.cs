using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
public class GrababbleItem : Item
{

    public enum PlayerSizeToHold { Small, Medium, Big, NotPossible };

    //left: size of object, Right: minimum Size of player
    public PlayerSizeToHold playerSizeToHoldSmall = PlayerSizeToHold.Small;
    public PlayerSizeToHold playerSizeToHoldMedium = PlayerSizeToHold.Medium;
    public PlayerSizeToHold playerSizeToHoldBig = PlayerSizeToHold.Big;

    [SerializeField]
    private float throwForce;
    
    private List<Collider> colliders = new List<Collider>();
    private Rigidbody rigidbody;

    private List<Collider> collidingObjectsWhileHeld = new List<Collider>();

    [Header("Audio")]
    [HideInInspector]
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip grabItemAudio;
    [SerializeField]
    private AudioClip dropItemAudio;


    // Start is called before the first frame update
    void Start()
    {
        colliders.AddRange(GetComponentsInChildren<Collider>());
        rigidbody = this.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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

        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
        rigidbody.isKinematic = true;

        audioSource.PlayOneShot(grabItemAudio);

        //Base collider used for trigger detection, which will be later used to see if you are allowed to drop item
        colliders[0].enabled = true;
        colliders[0].isTrigger = true;
    }

    virtual public bool Drop()
    {
        //Dont allow a drop if colliding with other objects
        if (collidingObjectsWhileHeld.Count > 0) return false; 
        audioSource.PlayOneShot(dropItemAudio);

        this.transform.parent = null;
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
        rigidbody.isKinematic = false;

        colliders[0].isTrigger = false;

        return true;
    }

    virtual public bool Throw()
    {
        if(Drop())
        {
            this.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
            return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        collidingObjectsWhileHeld.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        collidingObjectsWhileHeld.Remove(other);

    }
}
