using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnCollision : MonoBehaviour
{
    private JumperGameManager _jgm;

    // Start is called before the first frame update
    void Start()
    {
        var mainManager = GameObject.FindWithTag("MainManager");
        _jgm = mainManager.GetComponent<JumperGameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        ResetPlayer(other);
    }
    
    private void OnCollisionStay(Collision other)
    {
        ResetPlayer(other);
    }

    private void ResetPlayer(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _jgm.ResetPosition();
    }
}