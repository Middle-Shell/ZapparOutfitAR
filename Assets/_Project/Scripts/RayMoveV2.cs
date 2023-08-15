using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMoveV2 : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Draw")
        {
            _pointer.position = hit.point;
        }
        else
        {
            _pointer.position = new Vector3(0, 0, -4); // Перемещаем указатель из области видимости
        }
    }
}
