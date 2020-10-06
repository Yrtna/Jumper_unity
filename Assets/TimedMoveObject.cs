using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TimedMoveObject : MonoBehaviour
{
    public float maxTime;
    public float speed;
    public float maxPauseTime;

    private float _currentMoveTime;
    private float _currentPauseTime;
    public Vector3 dir;

    private bool _isPaused;

    private GameObject _player;
    private PlayerController _pc;

    public PlatformType _type;
    private JumperGameManager _jgm;

    // Start is called before the first frame update
    void Start()
    {
        _currentMoveTime = 0f;
        _currentPauseTime = 0f;
        var mainManager = GameObject.FindWithTag("MainManager");
        _jgm = mainManager.GetComponent<JumperGameManager>();
    }

    private void Update()
    {
        if (_isPaused)
        {
            WaitPause();
        }
        else if (!_isPaused)
        {
            TimeAndMove();
        }
    }

    private void WaitPause()
    {
        _currentPauseTime += Time.deltaTime;
        if (_currentPauseTime >= maxPauseTime)
        {
            _currentPauseTime = 0f;
            _isPaused = false;
        }
    }

    private void TimeAndMove()
    {
        _currentMoveTime += Time.deltaTime;
        if (_currentMoveTime >= maxTime)
        {
            dir *= -1;
            _currentMoveTime = 0f;
            _isPaused = true;
        }

        transform.Translate(dir * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_type == PlatformType.ClipPlayer && other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
            _pc = _player.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_type == PlatformType.ClipPlayer && other.gameObject.CompareTag("Player"))
        {
            _pc.IsGrounded = true;
            _player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_type == PlatformType.ClipPlayer && other.gameObject.CompareTag("Player"))
        {
            _player.transform.parent = null;
            _pc.IsGrounded = false;
            _pc = null;
            _player = null;
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (_type == PlatformType.Danger && other.gameObject.CompareTag("Player"))
    //     {
    //         _jgm.ResetPosition();
    //     }
    // }

    public enum PlatformType
    {
        Default,
        ClipPlayer,
        Danger,
        Physical
    }
}