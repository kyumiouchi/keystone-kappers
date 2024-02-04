using System.Collections;
using Game.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {

        #region Events

        public delegate void StartTouch(Vector2 position, float time);

        public event StartTouch OnStartTouch;

        public delegate void EndTouch(Vector2 position, float time);

        public event EndTouch OnEndTouch;

        #endregion

        private InputActions _inputActions;
        private Camera _mainCamera;
        private Coroutine _coroutine;

        private void Awake()
        {
            _inputActions = new InputActions();
            _mainCamera = Camera.main;
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

        private void StartTouchPrimary(InputAction.CallbackContext ctx)
        {
            _coroutine = StartCoroutine(StartTouchCache((float)ctx.startTime));
        }

        private void EndTouchPrimary(InputAction.CallbackContext ctx)
        {
            OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.time);
            StopCoroutine(_coroutine);
        }

        private IEnumerator StartTouchCache(float ctxStartTime)
        {
            yield return null; // Correct the first 0,0 position

            OnStartTouch?.Invoke(PrimaryPosition(), ctxStartTime);
        }

        public Vector2 PrimaryPosition()
        {
            return CameraPosition.ScreenToWorld(_mainCamera, _inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
        }
    }
}