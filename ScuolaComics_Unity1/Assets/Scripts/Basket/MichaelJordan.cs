using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelJordan : MonoBehaviour
{
    Camera _mainCam;
    Palla _pallaInMano;
    private void Awake()
    {
        _mainCam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0)) // tasto sinistro del mouse
        {
            // prova a prendere la palla
            _pallaInMano = TryToGetTheBall();
            // fermare la palla e renderla nostra figlia
            if (_pallaInMano != null)
            {
                StopTheBall();
                RendiFiglia();
            }
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            // se ho una palla, rilasciala
            if (_pallaInMano != null)
            {
                // lanciamo la palla
                Libera();
                LanciaPalla();
            }
        }
    }

    private Palla TryToGetTheBall()
    {
        Ray raggio = _mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        bool hoColpitoQualcosa = Physics.Raycast(raggio, out RaycastHit informazioni, 20);

        if (hoColpitoQualcosa)
        {
            Palla pallaColpita = informazioni.collider.gameObject.GetComponent<Palla>();
            if (pallaColpita != null )
            {
                // ho ottenuto davvero una palla
                return pallaColpita;
            }
        }

        return null;
    }

    private void LanciaPalla()
    {
        _pallaInMano.gameObject.GetComponent<Rigidbody>().AddForce(_mainCam.transform.forward * 1000);
    }

    private void StopTheBall()
    {
        Rigidbody rbDellaPalla = _pallaInMano.gameObject.GetComponent<Rigidbody>();
        rbDellaPalla.velocity = Vector3.zero;
        rbDellaPalla.isKinematic = true;
    }

    private void RendiFiglia()
    {
        _pallaInMano.gameObject.transform.SetParent(_mainCam.transform);
        _pallaInMano.IsCatch = true;
    }

    public void Libera()
    {
        _pallaInMano.IsCatch = false;
        _pallaInMano.gameObject.transform.SetParent(null);
        _pallaInMano.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
