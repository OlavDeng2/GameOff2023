using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Meower : MonoBehaviour
{
    [SerializeField]
    private InputActionReference meowAction;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip meowSound;
    void OnEnable()
    {
        meowAction.action.performed += Meow;

    }

    private void OnDisable()
    {
        meowAction.action.performed -= Meow;

    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Meow(InputAction.CallbackContext obj)
    {
        audioSource.PlayOneShot(meowSound);
    }
}
