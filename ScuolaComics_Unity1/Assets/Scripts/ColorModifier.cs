using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    [SerializeField] Color _newColor;
    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _newColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
