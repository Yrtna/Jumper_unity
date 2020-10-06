using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFireworks : MonoBehaviour
{
    public GameObject fireworks;

    public void SetFireworks(bool isActive)
    {
        fireworks.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SetFireworks(true);
    }
}