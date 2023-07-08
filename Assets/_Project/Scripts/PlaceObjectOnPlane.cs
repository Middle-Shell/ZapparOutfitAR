using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnPlane : MonoBehaviour
{
    public GameObject objectToPlace;
    public Camera arCamera; // Камера для отслеживания взаимодействия

    void Start()
    {
        StartCoroutine(DragAndDrop());
    }

    IEnumerator DragAndDrop()
    {
        while (true)
        {
            if (Input.touchCount <= 0)
                yield break;

            Touch touch = Input.GetTouch(0); // Получение первого касания

            if (touch.phase == TouchPhase.Began) // Проверка начала касания
            {

                yield return new WaitForSeconds(.5f);

                if (Input.touchCount != 1)
                    yield break;
                Ray ray = arCamera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out var hit)) // Проверка пересечения луча и объекта
                {
                    if (hit.transform.CompareTag("Plane")) // Проверка, что пересечение произошло с плоскостью
                    {
                        objectToPlace.transform.position = hit.point;
                    }
                }

            }
        }

    }
}
