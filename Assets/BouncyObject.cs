using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float reflectMultiplier = 1f;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * reflectMultiplier;
            }
        }
    }
}