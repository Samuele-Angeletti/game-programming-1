using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palla : MonoBehaviour
{
    GameManager _gameManager;
    public bool IsCatch = false;
    public void Inizializza(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCatch)
            return;

        Canestro canestro = other.gameObject.GetComponent<Canestro>();
        if (canestro != null)
        {
            _gameManager.AggiungiPunto();
        }
    }
}
