using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public float xOffset = 0;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.Translate(_playerTransform.position.x - transform.position.x + xOffset, 0,0);
    }
}
