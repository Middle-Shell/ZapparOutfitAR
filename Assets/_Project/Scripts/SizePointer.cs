using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizePointer : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _size = 0.01f;

    private float _scale;

    // Update is called once per frame
    void LateUpdate()
    {
        _scale = Vector3.Distance(transform.position, _cameraTransform.position);
        //transform.localScale = Vector3.one * _scale * _size;
    }
}
