using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions, PlayerControls.IPlayerInteractionMapActions
{
    public PlayerControls PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractionButtonPressed { get; private set; }
    public bool MenuButtonPressed { get; private set; }

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerLocomotionMap.Enable();
        PlayerControls.PlayerInteractionMap.Enable();
        PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
        PlayerControls.PlayerInteractionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.PlayerLocomotionMap.Disable();
        PlayerControls.PlayerInteractionMap.Disable();
        PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
        PlayerControls.PlayerInteractionMap.RemoveCallbacks(this);
    }

    private void LateUpdate()
    {
        InteractionButtonPressed = false;
        MenuButtonPressed = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnInteractionButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        InteractionButtonPressed = true;
    }

    public void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        MenuButtonPressed = true;
        GameflowManager.Instance.PauseGame(true);
    }
}
