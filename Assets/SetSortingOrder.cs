using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SetSortingOrder : MonoBehaviour
{
    public int sortingOrder = 0;

    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        _renderer.sortingOrder = sortingOrder;
    }

    private void OnValidate()
    {
        if (_renderer is null)
            _renderer = GetComponent<MeshRenderer>();
        _renderer.sortingOrder = sortingOrder;
    }
}