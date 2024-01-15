using Game.Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Rigidbody2D _rigidbody;
    private Vector3 _leftFlip = new Vector3(-1, 1, 1);
    private bool _isGround;
    [SerializeField] private SwipeDetection _inputDetection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputDetection = GetComponent<SwipeDetection>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigidbody.velocity = _inputDetection.GetStartPosition() * _speed;

        if (_inputDetection.GetStartPosition().x > 0.01f)
        {
            transform.localScale = _leftFlip;
        } else if (_inputDetection.GetStartPosition().x < -0.01f)
        {
            transform.localScale = Vector3.one;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)  && _isGround)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _speed);
        _isGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }
}
