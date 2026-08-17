using UnityEngine;
using UnityEngine.InputSystem.XR;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private PlayerLocomotionInput _playerLocomotionInput;
    [Space(5), Header("MovementSettings")]
    [SerializeField] private float _runAcceleration;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _drag;
    [SerializeField] private float _gravity;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayers;
    [Space(5), Header("CameraSettings")]
    [SerializeField] private float _lookSensHorizontal;
    [SerializeField] private float _lookSensVertical;
    [SerializeField] private float _lookLimitVertical;

    private float _footstepDistanceCounter = 0f;
    [SerializeField] private float _footstepInterval = 1.5f;

    private Vector2 _cameraRotation = Vector2.zero;
    private Vector2 _playerTargetRotation = Vector2.zero;
    private Vector3 _groundNormal;
    private Vector3 _newVelocity;
    private float _velocityY;

    private void Update()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        _cameraRotation.x += _lookSensHorizontal * _playerLocomotionInput.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - _lookSensVertical * _playerLocomotionInput.LookInput.y, -_lookLimitVertical, _lookLimitVertical);

        _playerTargetRotation.x += transform.eulerAngles.x + _lookSensHorizontal * _playerLocomotionInput.LookInput.x;
        transform.rotation = Quaternion.Euler(0, _playerTargetRotation.x, 0);

        _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0);
    }

    private void HandleMovement()
    {
        Vector3 cameraForward = new Vector3(_playerCamera.transform.forward.x, 0, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(_playerCamera.transform.right.x, 0, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRight * _playerLocomotionInput.MovementInput.x + cameraForward * _playerLocomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDirection * _runAcceleration * Time.deltaTime;
        _newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = _newVelocity.normalized * _drag * Time.deltaTime;
        _newVelocity = (_newVelocity.magnitude > _drag * Time.deltaTime) ? _newVelocity - currentDrag : Vector3.zero;
        _newVelocity = Vector3.ClampMagnitude(_newVelocity, _runSpeed);

        GroundCheck();

        _characterController.Move(_newVelocity * Time.deltaTime);

        if (_newVelocity.magnitude > 0.1f)
        {
            _footstepDistanceCounter += (_newVelocity * Time.deltaTime).magnitude;

            if (_footstepDistanceCounter >= _footstepInterval)
            {
                _footstepDistanceCounter = 0f;
                AudioHandler.Instance.PlayFootstep();
            }
        }
        else
        {
            _footstepDistanceCounter = 0f;
        }
    }

    private void GroundCheck()
    {
        if (_characterController.isGrounded && _velocityY < 0.0f)
        {
            _velocityY = -1f;
        }
        else
        {
            _velocityY += _gravity * -1 * Time.deltaTime;
        }

        _newVelocity.y = _velocityY;
    }
}
