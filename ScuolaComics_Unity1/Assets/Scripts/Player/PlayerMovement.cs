using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float force = 100;
    [SerializeField] float rotationSensibility = 3;
    Camera _mainCamera;
    InputActionSystem inputActions;
    Rigidbody _rigidBody;
    Vector3 _direction;
    Vector2 _rotation;
    private void Awake()
    {
        inputActions = new InputActionSystem();

        // inputActions.PlayerMovement.Forward.started += ForwardStarted;
        // inputActions.PlayerMovement.Forward.canceled += ForwardCanceled;

        // inputActions.PlayerMovement.Back.started += BackStarted;
        // inputActions.PlayerMovement.Back.canceled += BackCanceled;

        // inputActions.PlayerMovement.Left.started += LeftStarted;
        // inputActions.PlayerMovement.Left.canceled += LeftCanceled;

        // inputActions.PlayerMovement.Right.started += RightStarted;
        // inputActions.PlayerMovement.Right.canceled += RightCanceled;

        inputActions.PlayerRotation.MouseAxis.performed += MouseAxisPerformed;

        inputActions.PlayerMovementVector.Movement.performed += VectorMovementPerformed;
        // inputActions.PlayerMovementVector.Movement.canceled += VectorMovementCanceled;

        inputActions.Enable();

        _rigidBody = GetComponent<Rigidbody>();

        _mainCamera = Camera.main;
        _rotation = _mainCamera.transform.rotation.eulerAngles;
    }

    private void MouseAxisPerformed(InputAction.CallbackContext context)
    {
        _rotation += context.ReadValue<Vector2>().normalized;
    }

    private void ForwardCanceled(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(_direction.x, 0, 0);
    }

    private void ForwardStarted(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(_direction.x, 0, 1);
    }

    private void BackCanceled(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(_direction.x, 0, 0);
    }

    private void BackStarted(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(_direction.x, 0, -1);
    }

    private void LeftCanceled(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(0, 0, _direction.z);
    }

    private void LeftStarted(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(-1, 0, _direction.z);
    }

    private void RightCanceled(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(0, 0, _direction.z);
    }

    private void RightStarted(InputAction.CallbackContext obj)
    {
        _direction = new Vector3(1, 0, _direction.z);
    }

    private void VectorMovementPerformed(InputAction.CallbackContext obj)
    {
        //Alcuni Input si comportano in modo diverso
        // if(obj.phase == InputActionPhase.Performed) 
        //     Debug.Log("performed " + obj.ReadValue<Vector3>().ToString());
        // else if(obj.phase == InputActionPhase.Canceled)
        //     Debug.Log("canceled " + obj.ReadValue<Vector3>().ToString());

        _direction = obj.ReadValue<Vector3>();

        if(_direction == Vector3.zero)
            _rigidBody.velocity = Vector3.zero;
    }

    void Update()
    {
        _mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, _mainCamera.transform.position.z);
        
        //Raycast Non Alloc
        // Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);
        // RaycastHit[] hits = new RaycastHit[10];
        // Physics.RaycastNonAlloc(_mainCamera.transform.position, _mainCamera.transform.forward, hits, 1000, LayerMask.GetMask("Default"));

        // var raycastAlloc = Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out var hit, 1000, LayerMask.GetMask("Default"));
        // Debug.Log(hit);
    }

    private void FixedUpdate()
    {
        _rigidBody.AddForce(_direction * Time.fixedDeltaTime * force);
    }

    private void LateUpdate()
    {
        _mainCamera.transform.localRotation = Quaternion.Euler(-_rotation.y * rotationSensibility, _rotation.x * rotationSensibility, 0); 
        transform.rotation = Quaternion.Euler(0, _mainCamera.transform.rotation.eulerAngles.y, 0);
    }
}
