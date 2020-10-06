using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingPlatform : MonoBehaviour
{
    public float maxTimer;
    public bool isActive;

    public List<GameObject> progressNodes;

    private float _currentTimer;

    private MeshCollider _collider;
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<MeshCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        for (var i = 0; i < progressNodes.Count; i++)
        {
            if (!(_currentTimer >= i + 1)) continue;
            progressNodes[i].SetActive(true);
        }

        if (_currentTimer >= maxTimer)
        {
            _currentTimer = 0f;
            isActive = !isActive;
            _collider.enabled = isActive;
            _renderer.enabled = isActive;
            progressNodes.ForEach(s => s.SetActive(false));
        }
    }
}