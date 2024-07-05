using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] UnityEvent myEventOnTriggerEnter;
    [SerializeField] UnityEvent myEventOnTriggerExit;

    CameraSwapper _cameraSwapper;

    private void Awake()
    {
        _cameraSwapper = FindObjectOfType<CameraSwapper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            _cameraSwapper.SetTarget(player.gameObject);

            myEventOnTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            _cameraSwapper.SetTarget(null);

            myEventOnTriggerExit.Invoke();
        }
    }
}
