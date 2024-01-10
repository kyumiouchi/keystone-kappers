using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    #region Events
    public delegate void StartTouch (Vector2 position, float time);

    public event StartTouch OnStartTouch;
    public delegate void EndTouch (Vector2 position, float time);

    public event EndTouch OnEndTouch;
    #endregion

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }
    
    private void Start()
    {
        _inputActions.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _inputActions.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        // if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(Camera.main, pla));
    }
}
