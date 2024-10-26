using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine : MonoBehaviour
{

    [SerializeField] private Transform _stem;
    [SerializeField] private Transform _head;
    [SerializeField] private Rigidbody2D _headRB2D;
    [SerializeField] private SliderJoint2D _headSlider;
    [SerializeField] private HingeJoint2D _headLeftClaw;
    [SerializeField] private HingeJoint2D _headRightClaw;
    [SerializeField] private Rigidbody2D _headLeftClawRB2D;
    [SerializeField] private Rigidbody2D _headRightClawRB2D;
    [SerializeField] private float _clawTime;
    [SerializeField] private float _clawDelay;
    private float _runTimer, _retractTimer;
    private bool _running, _retracting;

    private void Start()
    {
        _headRB2D.bodyType = RigidbodyType2D.Static;
        _headSlider.enabled = false;
        _headLeftClaw.enabled = false;
        _headRightClaw.enabled = false;
        _headLeftClawRB2D.bodyType = RigidbodyType2D.Static;
        _headRightClawRB2D.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _running = true;
        }

        if (_head.position.y == -2f)
        {
            _running = false;
            _headLeftClaw.enabled = true;
            _headRightClaw.enabled = true;
            _headLeftClawRB2D.bodyType = RigidbodyType2D.Dynamic;
            _headRightClawRB2D.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Clawed());
        }

        if (_running)
        {
            _runTimer += Time.deltaTime;
            _stem.localScale = new Vector3(1f, Mathf.Lerp(1f, 5.875f, _runTimer / _clawTime), 1f);
            _head.position = Vector3.up * Mathf.Lerp(2.875f, -2f, _runTimer / _clawTime);
        }

        if (_retracting)
        {
            _retractTimer += Time.deltaTime;
            _stem.localScale = new Vector3(1f, Mathf.Lerp(5.875f, 1f, _retractTimer / _clawTime), 1f);
        }
    }

    private IEnumerator Clawed()
    {
        yield return new WaitForSeconds(_clawDelay);
        _retracting = true;
        _headRB2D.bodyType = RigidbodyType2D.Dynamic;
        _headSlider.enabled = true;
    }

}
