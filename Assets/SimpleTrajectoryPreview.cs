using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SimpleTrajectoryPreview : MonoBehaviour
{
    public int numberOfDots;

    public GameObject trajectoryGo;
    private readonly List<GameObject> _trajectoryPool = new List<GameObject>();

    private GameObject _player;
    private PlayerController _pc;
    private LineRenderer _lineRenderer;

    public SimulatedSceneLogic sceneLogic;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _pc = _player.GetComponent<PlayerController>();
        _lineRenderer = _pc.GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        if (_pc.AimIsActive)
        {
            var points = sceneLogic.simulatedTrajectory;
            _lineRenderer.SetPositions(points.ToArray());
            _lineRenderer.enabled = true;
        }
        else
            _lineRenderer.enabled = false;
    }

    private void DrawPoints()
    {
        if (_pc.AimIsActive)
        {
            var points = sceneLogic.simulatedTrajectory;
            Debug.Log($"SimulateTrajectory // nb: {points.Count}");
            for (var i = 0; i < _trajectoryPool.Count; i++)
            {
                if (i >= points.Count) return;
                var dot = _trajectoryPool[i];
                dot.transform.localPosition = points[i];
                dot.SetActive(true);
            }
        }
        else
            _trajectoryPool.ForEach(s => s.SetActive(false));
    }

    private void InitializeTrajectory()
    {
        for (var i = 0; i < numberOfDots; i++)
        {
            var dot = Instantiate(trajectoryGo, transform);
            dot.transform.position = Vector3.zero;
            dot.SetActive(false);
            _trajectoryPool.Add(dot);
        }
    }
}