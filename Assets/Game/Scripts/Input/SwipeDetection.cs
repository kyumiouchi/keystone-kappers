using System.Collections;
using UnityEngine;

namespace Game.Input
{
    public class SwipeDetection : MonoBehaviour
    {
        [SerializeField] private float _minimumDistance = 2f;
        [SerializeField] private float _maximumTime = 1f;
        [SerializeField, Range(0f, 1f)] private float _directionThreshold = .9f;
        private InputManager _inputManager;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private Vector2 _movePosition = Vector2.zero;
        private Coroutine _coroutine;

        private void Awake()
        {
            _inputManager = InputManager.Instance;
        }

        #region Callback

        private void OnEnable()
        {
            _inputManager.OnStartTouch += SwipeStart;
            _inputManager.OnEndTouch += SwipeEnd;
        }

        private void OnDisable()
        {
            _inputManager.OnStartTouch -= SwipeStart;
            _inputManager.OnEndTouch -= SwipeEnd;
        }

        #endregion

        private void SwipeStart(Vector2 position, float time)
        {
            _startPosition = position;

            _coroutine = StartCoroutine(Hold());
        }

        private IEnumerator Hold()
        {
            while (true)
            {
                _endPosition = _inputManager.PrimaryPosition();
                DetectSwipe();
                yield return null;
            }
        }

        private void SwipeEnd(Vector2 position, float time)
        {
            StopCoroutine(_coroutine);
            _endPosition = position;
            EndMovement();
        }

        private void DetectSwipe()
        {
            if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance)
            {
                Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);
                Vector3 direction = _endPosition - _startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                SwipeDetection2D(direction2D);
            }
        }

        private void SwipeDetection2D(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
            {
                StartMovement(Vector2.up);
            }
            else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
            {
                StartMovement(Vector2.down);
            }
            else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
            {
                StartMovement(Vector2.left);
            }
            else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
            {
                StartMovement(Vector2.right);
            }
        }


        private void StartMovement(Vector2 position)
        {
            _movePosition = position;
        }

        private void EndMovement()
        {
            _movePosition = Vector2.zero;
        }

        public Vector2 GetStartPosition()
        {
            return _movePosition;
        }
    }
}
