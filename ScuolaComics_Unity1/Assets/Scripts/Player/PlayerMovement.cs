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
        // da rispiegare un po' la differenza tra le position/rotation/scale e le localPosition/localRotation/localScale
        // da spiegare il Quaternione. Qui sono purtroppo andato un po' veloce perché abbiamo perso parecchio tempo a sistemare problemi con VS e Unity
        // L'obiettivo era quello di sparare dal player usanto il click del mouse.
        // manca il binding del click del mouse e tutta la parte di instantiate...
        // la ragazza online (Lisa) è un po' lenta e ha fatto perdere un po' di tempo a tutti quanti. Va un po' gestita perché si agita per nulla e fa casini. ad esempio non riusciva a creare una cartella.
        // è un po' più indietro e meno reattiva degli altri. 
        // Non ho fatto un GameManager perché mi sembrvaa già troppa roba tutto questo, quindi se vuoi puoi far spostare tutto questo codice di input da un'altra parte. 
        // non conoscono le Action e a malapena sanno i delegati. (te lo dico a titolo informativo) 
        // Gli ho però spiegato la collision matrix e la differenza tra i componenti 2d e 3d e la differenza anche delle due collision matrix. Gli ho fatto vedere che due oggetti possono collidere oppure no grazie a questa.
        // 
        // ps. dobbiamo assolutamente parlare con Alessio perché a questi ragazzi è stato chiesto di fare una build del gioco per il Visual Scripting senza che però venisse spiegato nulla.
        // non sapevano neanche la differenza tra un gameObject figlio e un Genitore però li hanno fatti sforzare per fare una build, senza neanche spiegargli cosa sono tutti i settaggi che vedono in Build Settings.
        // ritrovandosi tra l'altro a non capire come mai la build non gli funzionava in certe occasioni, o come mai una volta buildato le cose schiantavano... 
        // insomma, mi da fastidio che le cose importanti vengano fatte in maniera grossolana, perché se poi imparano a fare le cose male, poi si ritrovano a fare il doppio della fativa a dover re-imparare per bene come fare determinate cose.
        _mainCamera.transform.localRotation = Quaternion.Euler(-_rotation.y * rotationSensibility, _rotation.x * rotationSensibility, 0); 
    }
}
