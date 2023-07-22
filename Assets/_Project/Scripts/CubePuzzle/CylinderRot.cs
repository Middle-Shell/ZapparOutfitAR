using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CylinderRot : MonoBehaviour
{
    [SerializeField] private Rigidbody _Poiter;
    private Rigidbody _ball;
    public  bool _rTouchCheck = false;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "point")
        {
            _rTouchCheck = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "point")
        {
            _rTouchCheck = false;
        }

    }

    private void Update()
    {
        _ball = GetComponent<Rigidbody>();
        if (_rTouchCheck)
        {
            Vector3 FollowDirection = (transform.position - _Poiter.position).normalized;
            _ball.AddForce(FollowDirection);
        }


    }
}

