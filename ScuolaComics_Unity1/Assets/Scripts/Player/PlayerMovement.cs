using System;
using System.Collections;
using System.Collections.Generic;
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

        inputActions.PlayerMovement.Forward.started += ForwardStarted;
        inputActions.PlayerMovement.Forward.canceled += ForwardCanceled;

        inputActions.PlayerMovement.Back.started += BackStarted;
        inputActions.PlayerMovement.Back.canceled += BackCanceled;

        inputActions.PlayerMovement.Left.started += LeftStarted;
        inputActions.PlayerMovement.Left.canceled += LeftCanceled;

        inputActions.PlayerMovement.Right.started += RightStarted;
        inputActions.PlayerMovement.Right.canceled += RightCanceled;

        inputActions.PlayerRotation.MouseAxis.performed += MouseAxisPerformed;

        _rigidBody = GetComponent<Rigidbody>();

        inputActions.Enable();

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

    private void FixedUpdate()
    {
        _rigidBody.AddForce(_direction * Time.fixedDeltaTime * force);
    }

    private void LateUpdate()
    {
        
        _mainCamera.transform.localRotation = Quaternion.Euler(-_rotation.y * rotationSensibility, _rotation.x * rotationSensibility, 0); 
    }
}
