using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingChain : MonoBehaviour
{
    Rigidbody2D rb;
    HingeJoint2D hj;
    float motorSpeed;
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        motorSpeed = hj.motor.motorSpeed;
    }
    void Update() 
    {
     
      
    }
    private void FixedUpdate()
    {
      //  print("position: " + rb.transform.rotation.z);
        JointMotor2D motor = hj.motor;
        if (rb.transform.rotation.z <= (-0.49f))
        {
            motorSpeed *= (-1);
            motor.motorSpeed = motorSpeed;
            hj.motor = motor;
        }
        else if (rb.transform.rotation.z >= 0.49f)
        {
            motorSpeed *= (-1);
            motor.motorSpeed = motorSpeed;
            hj.motor = motor;
        } 
    }
}

