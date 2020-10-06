using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forceMultiplier = 1f;
    public float minDragDistance = 0.5f;
    public float maxDragDistance = 2.5f;

    private Vector2 _clickPosition;
    private Vector2 _currentDragPosition;
    private Vector2 _releasePosition;
    public Vector2 dir;

    public float TrueMag
    {
        get
        {
            if (dir.magnitude < minDragDistance)
                return 0f;
            if (dir.magnitude > maxDragDistance)
                return maxDragDistance;
            return dir.magnitude;
        }
    }

    private Rigidbody _rigidbody;
    private TrailRenderer _trailRenderer;

    public bool AimIsActive { get; private set; }

    public bool canLaunch = false;

    public bool IsGrounded { get; set; }

    private Quaternion originalRotation;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        Debug.Log($"Velocity ({_rigidbody.velocity.x}, {_rigidbody.velocity.y}, {_rigidbody.velocity.z})");
        canLaunch = IsGrounded || _rigidbody.velocity.y == 0;
        if (canLaunch && transform.rotation != originalRotation)
            transform.rotation = originalRotation;
    }

    private void OnMouseDown()
    {
        _clickPosition = Input.mousePosition;
        // _canDrag = IsGrounded || _rigidbody.velocity.y == 0;

        var playerClick = Camera.main.WorldToScreenPoint(gameObject.transform.position) + Vector3.forward;
        var clickPos = Camera.main.ScreenToWorldPoint(new Vector3(_clickPosition.x, _clickPosition.y, 1));
        // _lineRenderer.SetPositions(new[] {gameObject.transform.position, gameObject.transform.position});
        // _lineRenderer.SetPositions(new[] {clickPos, clickPos});
        // _lineRenderer.SetPositions(new[] {Vector3.zero, Vector3.zero});
        // _lineRenderer.SetPositions(new[] {playerClick, playerClick});
    }

    private void OnMouseDrag()
    {
        _currentDragPosition = Input.mousePosition;

        var playerClick = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var clickPos = Camera.main.ScreenToWorldPoint(new Vector3(playerClick.x, playerClick.y, 1));
        var dragPos = Camera.main.ScreenToWorldPoint(new Vector3(_currentDragPosition.x, _currentDragPosition.y, 1));

        var dir = dragPos - clickPos;
        var finalDir = dir;
        if (dir.magnitude > maxDragDistance)
            finalDir = dir.normalized * maxDragDistance;
        this.dir = finalDir;

        if (canLaunch && dir.magnitude >= minDragDistance)
            AimIsActive = true;
        else AimIsActive = false;
    }

    private void OnMouseUp()
    {
        if (!canLaunch) return;
        _releasePosition = Input.mousePosition;

        AimIsActive = false;
        if (this.dir.magnitude < minDragDistance) return;
        var finalDir = -this.dir;

        if (finalDir.magnitude > maxDragDistance)
            finalDir = finalDir.normalized * maxDragDistance;
        _rigidbody.AddForce(finalDir * forceMultiplier);
        canLaunch = false;
    }

    private Vector2 ForceDirection()
    {
        return _clickPosition - _releasePosition;
    }

    public Vector2 GenerateForce()
    {
        return -(dir.normalized * TrueMag) * forceMultiplier;
    }

    public void ResetPlayer()
    {
        _trailRenderer.enabled = false;
        _trailRenderer.Clear();
        transform.rotation = originalRotation;
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _trailRenderer.enabled = true;
    }
}