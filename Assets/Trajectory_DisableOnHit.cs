using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory_DisableOnHit : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
