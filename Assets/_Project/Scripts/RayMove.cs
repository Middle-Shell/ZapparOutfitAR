using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMove : MonoBehaviour
{
    [SerializeField] private Transform _pointer;

    [SerializeField] private GameObject _ball;
    private Ray _ray;
    [SerializeField] private float movementSpeed = 0.1f;
    private int _layerHit;
    private RaycastHit _hit;
    
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position,transform.forward*100,Color.green);

        if (Physics.Raycast(ray, out hit) && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Box")
        {
            Vector3 targetPosition = hit.point;
            Debug.Log(targetPosition);
            if (Vector3.Distance(targetPosition,_ball.transform.position) < 5)
            {
                Vector3 moveDirection = targetPosition - transform.position;
                //_ball.velocity = moveDirection * movementSpeed;
                _ball.transform.position = new Vector3(
                    Mathf.Lerp(_ball.transform.position.x, targetPosition.x, 0.5f * Time.deltaTime),
                    (Mathf.Lerp(_ball.transform.position.y, targetPosition.y, 0.5f * Time.deltaTime)),
                    (Mathf.Lerp(_ball.transform.position.z, targetPosition.z, 0.5f * Time.deltaTime)));
                _pointer.position = _hit.point;
            }
        }
        else
        {
           _pointer.position = new Vector3(0,0,-4);
        }
    }
}
