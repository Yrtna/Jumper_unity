using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class DebugManager : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _player.transform.position = new Vector3(52, 0, 0);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            _player.transform.position = new Vector3(148, 4, 0);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            _player.transform.position = new Vector3(200, 4, 0);
        }
    }
}
#endif