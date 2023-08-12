using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMoveV2 : MonoBehaviour
{
    [SerializeField] private Transform _pointer;
    public Rigidbody _ballRigidbody;
    [SerializeField] private GameObject _ball;
    [SerializeField] private float movementSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        _ballRigidbody = _ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 100, Color.green);

        if (Physics.Raycast(ray, out hit) && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Box")
        {
            Vector3 targetPosition = hit.point;

            // Вычисляем направление и скорость движения
            Vector3 moveDirection = targetPosition - _ball.transform.position;
            Vector3 moveVelocity = moveDirection.normalized * movementSpeed;

            // Применяем физическую силу к Rigidbody для движения
            _ballRigidbody.velocity = moveVelocity;

            _pointer.position = hit.point;
        }
        else
        {
            // Если луч не попадает на объект с тегом "Box" или выходит за пределы дистанции raycastDistance
            _ballRigidbody.velocity = Vector3.zero; // Останавливаем движение _ball
            _pointer.position = new Vector3(0, 0, -4); // Перемещаем указатель из области видимости
        }
    }
}
