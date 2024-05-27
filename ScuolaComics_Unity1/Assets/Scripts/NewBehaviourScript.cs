using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Ciao! Io sono l'Awake");
    }

    private void OnEnable()
    {
        Debug.Log("Io sono OnEnable");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Qui siamo allo Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning("Attenzione, siamo in Update!");
    }

    private void FixedUpdate()
    {
        Debug.LogWarning("Occhio, siamo anche nel FixedUpdate");
    }

    private void LateUpdate()
    {
        Debug.LogWarning("Qui invece siamo nel LateUpdate");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Qualcuno mi ha triggerato");
    }

    private void OnDisable()
    {
        Debug.LogWarning("Mi sto spegnendo");
    }

    private void OnDestroy()
    {
        Debug.LogError("Sono morto!");
    }
}
