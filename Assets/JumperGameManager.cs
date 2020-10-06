using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperGameManager : MonoBehaviour
{
    private GameObject _player;
    private PlayerController _pc;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _pc = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        _pc.ResetPlayer();
    }
}
