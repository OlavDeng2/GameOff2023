using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private GameObject endLevelScreen;


    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private StarterAssetsInputs starterAssetsInputs;
    // Start is called before the first frame update
    void Start()
    {
        endLevelScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if player
        if (other.GetComponent<FirstPersonController>() == null) return;

        playerInput.SwitchCurrentActionMap("InMenu");
        //Mouse Input is locked to game
        starterAssetsInputs.Pause(true);
        endLevelScreen.SetActive(true);
    }
}
