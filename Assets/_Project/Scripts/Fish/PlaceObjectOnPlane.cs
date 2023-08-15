using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private List<GameObject> _builds;
    [SerializeField] private GameObject SelectPanel;
    [SerializeField] private GameObject DrawPanel;
    [SerializeField] private Camera arCamera; // Камера для отслеживания взаимодействия

    void Update()
    {
        if(Input.touchCount <= 0)
            return;
        
        Touch touch = Input.GetTouch(0); // Получение первого касания

        if (touch.phase == TouchPhase.Moved && Input.touchCount == 1) // Проверка начала касания
        {
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

    public void SelectBuild(int num)
    {
        objectToPlace = _builds[num];
        objectToPlace.SetActive(true);
        SelectPanel.SetActive(false);
        DrawPanel.SetActive(true);
    }
}
