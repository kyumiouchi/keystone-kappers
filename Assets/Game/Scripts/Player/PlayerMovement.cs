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
    }

    private void FixedUpdate()
    {
        PlayerMove();

        ChangeFlip();
    }

    private void ChangeFlip()
    {
        if (_inputDetection.GetStartPosition().x > 0.01f)
        {
            transform.localScale = _leftFlip;
        } else if (_inputDetection.GetStartPosition().x < -0.01f)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void PlayerMove()
    {
        if (_inputDetection.GetStartPosition() == Vector2.left
            // || _inputDetection.GetStartPosition() == Vector2.right
            )
        {
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);//_inputDetection.GetStartPosition() * _speed;
        } 
        else if (_inputDetection.GetStartPosition() == Vector2.right  && _isGround)
        {
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        } 
        
        if (_inputDetection.GetStartPosition() == Vector2.up  && _isGround)
        {
            Jump();
        } 
        else if (_inputDetection.GetStartPosition() == Vector2.down && _isGround)
        {
            Squat();
        }
    }

    private void Squat()
    {
        Debug.Log("Squat");
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
