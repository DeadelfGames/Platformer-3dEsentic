using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _friction;

    private bool _isGrounded;
    private float _maxSpeed = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
                _rigidbody.AddForce(0f, _jumpSpeed, 0f, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        float speedMultiplier = 1f;

        if (_isGrounded == false)
            speedMultiplier = 0.1f;

        if (_rigidbody.velocity.x > _maxSpeed && Input.GetAxis("Horizontal") > 0)
            speedMultiplier = 0f;
        else if (_rigidbody.velocity.x < -_maxSpeed && Input.GetAxis("Horizontal") < 0)
            speedMultiplier = 0f;

        _rigidbody.AddForce(Input.GetAxis("Horizontal") * _moveSpeed * speedMultiplier, 0, 0, ForceMode.VelocityChange);

        if (_isGrounded)
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0, 0, ForceMode.VelocityChange);
    }


    private void OnCollisionStay(Collision other)
    {
        for (int i = 0; i < other.contactCount; i++)
        {
            float angle = Vector3.Angle(other.contacts[i].normal, Vector3.up);

            if (angle < 45f)
                _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        _isGrounded = false;
    }
}
