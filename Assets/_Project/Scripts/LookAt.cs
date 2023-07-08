using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        direction = m_cameraTransform.position - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }
}
