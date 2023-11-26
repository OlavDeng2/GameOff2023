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
    private GameObject objectToPlace;
    [SerializeField]
    private UnityEvent OnPlace;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(objectToPlace == other.gameObject)
        {
            audioSource.PlayOneShot(placeAudio);
            OnPlace?.Invoke();
        }
    }
}
