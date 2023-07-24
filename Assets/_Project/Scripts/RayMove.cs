using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    [SerializeField] private Transform _pointer;

    private Ray _ray;

    private RaycastHit _hit;
    // Update is called once per frame
    void Update()
    {
        _ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.yellow);
        if (Physics.Raycast(_ray, out _hit))
        {
            _pointer.position = _hit.point;
        }
    }
}
