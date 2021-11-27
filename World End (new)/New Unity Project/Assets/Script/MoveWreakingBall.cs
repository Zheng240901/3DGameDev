﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveWreakingBall : MonoBehaviour
{
    [SerializeField] private float startVelocity;
    private float currentAngle,currentVelocity;
    private JointMotor motor;
    private HingeJoint hingeJointProperties;
    private bool canAssignMotor;
    public static bool isSwingingLeft;
    public static bool isSwingingRight;
    //Rigidbody rb;
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        hingeJointProperties = GetComponent<HingeJoint>();
        hingeJointProperties.useMotor = true;
        motor = hingeJointProperties.motor;
        motor.targetVelocity = startVelocity;
        hingeJointProperties.motor = motor;
        currentVelocity = motor.targetVelocity;
    }


    void Update()
    {
        currentAngle = hingeJointProperties.angle;
        //Debug.Log("Current Angle : " + currentAngle);
        if (currentVelocity == Math.Abs(startVelocity) && currentAngle >= 40)
        {
            ChangeDirection();
            isSwingingLeft = true;
            isSwingingRight = false;
            //Debug.Log(isSwingingLeft);
        }
        else if (currentVelocity == -Math.Abs(startVelocity) && currentAngle <= -45)
        {
            ChangeDirection();
            isSwingingRight = true;
            isSwingingLeft = false;
            //Debug.Log(isSwingingRight);
        }

    }

    private void FixedUpdate()
    {
        if (canAssignMotor)
        {
            motor.targetVelocity = currentVelocity;
            hingeJointProperties.motor = motor;
            canAssignMotor = false;
        }

    }

    void ChangeDirection()
    {
        currentVelocity *= -1;
        canAssignMotor = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IKnockback knockable = collision.gameObject.GetComponent<IKnockback>();
        if (knockable != null)
        {
            knockable.DoKnockback();
        }
    }
}
