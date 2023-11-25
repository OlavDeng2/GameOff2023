using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class IngameMenu : MenuManager
{
    
    [SerializeField]
    private GameObject ingameMenu;
    [SerializeField]
    private InputActionReference openMenuAction;
    [SerializeField]
    private InputActionReference closeMenuAction;

    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private StarterAssetsInputs starterAssetsInputs;
    
    private void OnEnable()
    {
        openMenuAction.action.performed += ToggleIngameMenu;
        closeMenuAction.action.performed += ToggleIngameMenu;

    }
    private void OnDisable()
    {
        openMenuAction.action.performed -= ToggleIngameMenu;
        closeMenuAction.action.performed -= ToggleIngameMenu;


    }

    public void CloseIngameMenu()
    {
        ToggleIngameMenu(new InputAction.CallbackContext());
    }

    private void ToggleIngameMenu(InputAction.CallbackContext obj)
    {
        if(currentPanel == ingameMenu)
        {
            SwitchPanel(startPanel);
            playerInput.SwitchCurrentActionMap("Player");
            //Mouse Input is locked to game
            starterAssetsInputs.Pause(false);
        }

        else
        {
            SwitchPanel(ingameMenu);
            playerInput.SwitchCurrentActionMap("InMenu");
            //Mouse Input is locked to UI
            starterAssetsInputs.Pause(true);
        }
    }
}
