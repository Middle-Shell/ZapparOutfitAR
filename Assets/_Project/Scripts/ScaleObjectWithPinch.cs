using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleObjectWithPinch : MonoBehaviour
{
    private Vector2 m_initialTouch1Pos;
    private Vector2 m_initialTouch2Pos;
    private Vector3 m_initialScale;
    private float m_initialDistance;
    private Touch m_touch1;
    private Touch m_touch2;
    private Vector2 m_currentTouch1Pos;
    private Vector2 m_currentTouch2Pos;
    [SerializeField] private Image _imageSignal;

    void Start()
    {
        m_initialScale = transform.localScale;
    }

    void Update()
    {
        if (Input.touchCount == 2) // Проверка наличия двух касаний (щипка)
        {
            _imageSignal.color = Color.green;
            m_touch1 = Input.GetTouch(0);
            m_touch2 = Input.GetTouch(1);

            if (m_touch2.phase == TouchPhase.Began) // Если одно из касаний только началось
            {
                m_initialTouch1Pos = m_touch1.position;
                m_initialTouch2Pos = m_touch2.position;
                m_initialDistance = Vector2.Distance(m_initialTouch1Pos, m_initialTouch2Pos);
            }
            else if (m_touch1.phase == TouchPhase.Moved && m_touch2.phase == TouchPhase.Moved) // Если оба касания двигаются
            {
                m_currentTouch1Pos = m_touch1.position;
                m_currentTouch2Pos = m_touch2.position;
                float currentDistance = Vector2.Distance(m_currentTouch1Pos, m_currentTouch2Pos);

                float scaleFactor = currentDistance / m_initialDistance;
                transform.localScale = m_initialScale * scaleFactor;
            }
        }
    }
}
