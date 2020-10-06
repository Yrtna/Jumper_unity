using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrevisionTrajectory : MonoBehaviour
{
    public int minTrajectoryPoolSize;

    public float timeInterval = 0.2f;
    private float _currentTime = 0f;

    public GameObject trajectoryGo;
    private readonly List<GameObject> _trajectoryPool = new List<GameObject>();

    private GameObject _player;
    private PlayerController _pc;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _pc = _player.GetComponent<PlayerController>();
        InitializeTrajectory();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pc.canLaunch && _pc.TrueMag >= _pc.minDragDistance && _pc.AimIsActive)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= timeInterval)
            {
                _currentTime = 0f;
                QueueNextTrajectoryNode();
            }
        }
        else
        {
            _trajectoryPool.ForEach(s => s.SetActive(false));
            _currentTime = 0f;
        }
    }

    private void InitializeTrajectory()
    {
        for (var i = 0; i < minTrajectoryPoolSize; i++)
        {
            _trajectoryPool.Add(NewNode());
        }
    }

    private void QueueNextTrajectoryNode()
    {
        var unUsed = _trajectoryPool.FirstOrDefault(s => s.gameObject.activeSelf == false);
        if (unUsed == null)
        {
            unUsed = NewNode();
            _trajectoryPool.Add(unUsed);
        }

        unUsed.SetActive(true);
        var dir = _pc.dir.normalized;
        unUsed.transform.position = _player.transform.position;//+ (-new Vector3(dir.x, dir.y, 0) * (_pc.minDragDistance * 2));
        var rb = unUsed.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(_pc.GenerateForce());
    }

    private GameObject NewNode()
    {
        var go = Instantiate(trajectoryGo);
        go.SetActive(false);
        return go;
    }
}