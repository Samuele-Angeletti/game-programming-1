using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float force = 100;
    [SerializeField] float rotationSensibility = 3;
    [SerializeField] private float verticalRotationSensibility = 1.5f;
    [SerializeField] private float horizontalRotationSensibility = 3;
    Camera _mainCamera;
    InputActionSystem inputActions;
    Rigidbody _rigidBody;
    Vector3 _direction;
    Vector2 _rotation;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    [SerializeField] private int maxCollisionDetection = 10;

    [SerializeField] private float clampRange = 45f;

    [SerializeField] private bool grounded = false;

    private void Awake()
    {
        inputActions = new InputActionSystem();

        inputActions.PlayerRotation.MouseAxis.performed += MouseAxisPerformed;
        inputActions.PlayerRotation.MouseAxis.canceled += MouseAxisPerformed;

        inputActions.PlayerMovementVector.Movement.performed += VectorMovementPerformed;
        // inputActions.PlayerMovementVector.Movement.canceled += VectorMovementCanceled;

        inputActions.Enable();

        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _rotation = _mainCamera.transform.rotation.eulerAngles;
    }

    private void MouseAxisPerformed(InputAction.CallbackContext context)
    {
        _rotation = context.ReadValue<Vector2>();

        if(context.phase == InputActionPhase.Canceled)
        {
            // Debug.Log("Mouse canceled");
            _rotation = Vector2.zero;
        }
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
        CameraFollowPlayer();
        
        // Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);

        // Raycast Non Alloc
        // var raycastNonAlloc = RaycastNonAlloc(maxCollisionDetection);
        // 
        // Raycast Alloc
        // var raycastAlloc = RaycastAlloc();

        grounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), 
                                    -transform.up, 
                                    out RaycastHit hitInfo, 
                                    3f, 
                                    LayerMask.GetMask("Ground"));

        if(grounded)
        {
            Debug.Log("Grounded");
        }
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void LateUpdate()
    {
        RotationUpdate();
    }

    private void RotationUpdate()
    {
        // Debug.Log("deltatime " + Time.deltaTime);
        // Debug.Log("smoothdeltatime " + Time.smoothDeltaTime);
        horizontalRotation += _rotation.x * horizontalRotationSensibility * Time.smoothDeltaTime;
        verticalRotation += _rotation.y * verticalRotationSensibility * Time.smoothDeltaTime;

        // verticalRotation = math.clamp(verticalRotation, -45, 45);
        verticalRotation = Mathf.Clamp(verticalRotation, -clampRange, clampRange);

        _mainCamera.transform.localRotation = Quaternion.Euler(-verticalRotation, horizontalRotation, 0); 
        transform.rotation = Quaternion.Euler(0, _mainCamera.transform.rotation.eulerAngles.y, 0);
    }

    private void MovementUpdate()
    {
        var moveDirection = 
            math.normalizesafe(transform.forward) * _direction.z 
            + math.normalizesafe(transform.right) * _direction.x;

        // _rigidBody.AddForce(moveDirection * Time.fixedDeltaTime * force);

        _rigidBody.velocity = math.normalizesafe(moveDirection) * Time.fixedDeltaTime * force;
    }

    private void CameraFollowPlayer()
    {
        _mainCamera.transform.position = 
            new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    private RaycastHit[] RaycastNonAlloc(int maxCollisions = 10)
    {
        RaycastHit[] hits = new RaycastHit[maxCollisions];
        Physics.RaycastNonAlloc(_mainCamera.transform.position, _mainCamera.transform.forward, hits, 1000, LayerMask.GetMask("Default"));

        foreach(var hit in hits)
        {
            // if(hit.collider) Debug.Log("NON ALLOC: " + hit.collider.gameObject.name);
        }

        return hits;
    }

    private RaycastHit[] RaycastAlloc()
    {
        RaycastHit[] hits = Physics.RaycastAll(_mainCamera.transform.position, _mainCamera.transform.forward, 1000, LayerMask.GetMask("Default"));
        
        // foreach(var hit in hits)
        // {
        //     if(hit.collider)
        //     {
        //         if(hit.collider.gameObject.TryGetComponent(out IDamageable damageable))
        //         {
        //             damageable.TakeDamage(1);
        //         }

        //         Debug.Log("ALLOC: " + hit.collider.gameObject.name);
        //     }
        // }
        return hits;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Vector3.down * 1f);
    }
}


