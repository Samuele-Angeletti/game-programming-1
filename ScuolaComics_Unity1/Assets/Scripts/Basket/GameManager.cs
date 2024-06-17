using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // voglio spawnare MichaelJordan
    [SerializeField] MichaelJordan michaelJordanPrefab;

    // voglio spawnare una palla ogni volta che voglio con un tasto
    [SerializeField] Palla pallaPrefab;

    int _score = 0;

    private void Awake()
    {
        MichaelJordan newMichaelJordan = Instantiate(michaelJordanPrefab);

        newMichaelJordan.transform.position = Vector3.zero;

    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            // crea una palla
            CreaPalla();
        }
    }

    private void CreaPalla()
    {
        Palla newPalla = Instantiate(pallaPrefab);

        Vector3 randomPosition = Vector3.one;

        float randomX = Random.Range(-1.5f, 1.5f);
        float randomZ = Random.Range(-1.5f, 1.5f);

        randomPosition = new Vector3(randomX, 1, randomZ);

        newPalla.transform.position = randomPosition;

        newPalla.Inizializza(this);
    }

    internal void AggiungiPunto()
    {
        _score++;

        Debug.Log("Canestro! " + _score);
    }
}
