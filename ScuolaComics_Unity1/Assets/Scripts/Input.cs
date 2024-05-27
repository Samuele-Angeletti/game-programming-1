using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxVelocity;
    public bool TestMyProperty;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        if (_rigidBody == null)
        {
            Debug.LogError("Non c'è alcun RigidBody attaccato a questo Oggetto!");
            return;
        }

        _rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // DOBBIAMO USARE IL RIGIDBODY!!!

        //if (UnityEngine.Input.GetKey(KeyCode.W))
        //{
        //    //gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, 1); sono la stessa cosa
        //    gameObject.transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime; // come sopra
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.S))
        //{
        //    //gameObject.transform.position += new Vector3(0, 0, -1); sono la stessa cosa
        //    gameObject.transform.position -= new Vector3(0, 0, 1) * speed * Time.deltaTime; // equivalente a sopra
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.D))
        //{
        //    gameObject.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.A))
        //{
        //    //gameObject.transform.position += new Vector3(-1, 0, 0); sono la stessa cosa
        //    gameObject.transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime; // equivalente a sopra
        //}

        if (Mathf.Abs(_rigidBody.velocity.magnitude) >= _maxVelocity)
        {
            //_rigidBody.velocity = qualcosa...;
        }
    }

    private void FixedUpdate()
    {
        if (UnityEngine.Input.GetKey(KeyCode.W))
        {
            _rigidBody.AddForce(GetCalculatedForce(Vector3.forward));
        }
        if (UnityEngine.Input.GetKey(KeyCode.S))
        {
            _rigidBody.AddForce(GetCalculatedForce(Vector3.back));
        }
        if (UnityEngine.Input.GetKey(KeyCode.D)) 
        {
            _rigidBody.AddForce(GetCalculatedForce(Vector3.right));
        }
        if (UnityEngine.Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddForce(GetCalculatedForce(Vector3.left));
        }
    }

    private Vector3 GetCalculatedForce(Vector3 direction)
    {
        return Time.fixedDeltaTime * _speed * direction;
    }
}
