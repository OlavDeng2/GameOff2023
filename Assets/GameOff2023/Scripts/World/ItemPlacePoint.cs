using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(AudioSource))]
public class ItemPlacePoint : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip placeAudio;
    [SerializeField]
    private AudioClip removeAudio;
    [SerializeField]
    private GameObject objectToPlace;
    [SerializeField]
    private UnityEvent OnPlace;
    [SerializeField]
    private UnityEvent OnRemove;

    bool itemPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (objectToPlace == other.gameObject)
        {
            if(other.GetComponent<GrababbleItem>() != null)
            {
                if (other.GetComponent<GrababbleItem>().isHeld) return;
                if (itemPlaced) return;
                audioSource.PlayOneShot(placeAudio);
                OnPlace?.Invoke();
                itemPlaced = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectToPlace == other.gameObject)
        {
            if (!itemPlaced) return;

            audioSource.PlayOneShot(removeAudio);
            OnRemove?.Invoke();
            itemPlaced = false;
        }    
    }
}
