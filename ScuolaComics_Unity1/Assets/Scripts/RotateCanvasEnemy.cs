using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvasEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        if(player != null) transform.LookAt(player);
    }
}
