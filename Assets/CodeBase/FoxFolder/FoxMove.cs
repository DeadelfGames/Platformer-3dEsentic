using UnityEngine;

public class FoxMove : MonoBehaviour
{
    [SerializeField] private float _foxSpeed = 5f;
    [SerializeField] private float _foxFriction = 1f;
    [SerializeField] private float _foxJumpForce = 4f;
    [SerializeField] private Rigidbody _foxRigidbody;

    private bool _isFoxGrounded;
    private float _foxMaxSpeed = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isFoxGrounded)
                _foxRigidbody.AddForce(0f, _foxJumpForce, 0f, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        float foxspeedMultiplier = 1f;
        float horizontal = Input.GetAxis("Horizontal") * _foxSpeed;

        if (_isFoxGrounded == false)
            foxspeedMultiplier = 0.1f;

        if (_foxRigidbody.velocity.x > _foxMaxSpeed && horizontal > 0f)
            foxspeedMultiplier = 0f;
        else if (_foxRigidbody.velocity.x < -_foxMaxSpeed && horizontal < 0f)
            foxspeedMultiplier = 0f;

        _foxRigidbody.AddForce(horizontal * foxspeedMultiplier, 0f, 0f, ForceMode.VelocityChange);

        if (_isFoxGrounded)
            _foxRigidbody.AddForce(-_foxRigidbody.velocity.x * _foxFriction, 0f, 0f, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision other)
    {
        for (int i = 0; i < other.contactCount; i++)
        {
            float angle = Vector3.Angle(other.contacts[i].normal, Vector3.up);

            if (angle < 45)
                _isFoxGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _isFoxGrounded = false;
    }
}