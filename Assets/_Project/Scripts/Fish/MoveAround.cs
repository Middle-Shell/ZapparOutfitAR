using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private int m_anglePerSecond = -10;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(m_cameraTransform.position, Vector3.up, m_anglePerSecond * Time.deltaTime);
    }
}
