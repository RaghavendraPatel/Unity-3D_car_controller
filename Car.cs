using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool breakPressed;
    private bool shiftPressed;
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float currentbreakForce;
    private float currentMotorForce;

    [SerializeField] private WheelCollider fr, fl, bl, br;
    [SerializeField] private Transform frt, flt, brt, blt;
    [SerializeField] private float maxSteerAngle = 30;
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        breakPressed = Input.GetKey(KeyCode.Space);
        shiftPressed = Input.GetKey(KeyCode.LeftShift);
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        fr.steerAngle = steeringAngle;
        fl.steerAngle = steeringAngle;

    }

    private void Accelerate()
    {
        currentMotorForce = shiftPressed ? motorForce * 10 : motorForce; 
        br.motorTorque = verticalInput * currentMotorForce;
        bl.motorTorque = verticalInput * currentMotorForce;
        currentbreakForce = breakPressed ? breakForce : 0f;
        Applybreak();
    }
    private void Applybreak()
    {
        fr.brakeTorque = currentbreakForce;
        fl.brakeTorque = currentbreakForce;
        br.brakeTorque = currentbreakForce;
        bl.brakeTorque = currentbreakForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(fr, frt);
        UpdateWheelPose(fl, flt);
        UpdateWheelPose(br, brt);
        UpdateWheelPose(bl, blt);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quart = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quart);
        _transform.position = _pos;
        _transform.rotation = _quart;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
    }
    private void Update()
    {
        UpdateWheelPoses();
    }
}
