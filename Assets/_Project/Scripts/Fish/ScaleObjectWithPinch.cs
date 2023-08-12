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
    private Quaternion _YRotation;

    void Start()
    {
        m_initialScale = transform.localScale;
    }

    void Update()
    {

        if (Input.touchCount == 1)
        {
            m_touch1 = Input.GetTouch(0);
            m_currentTouch1Pos = m_touch1.position;
            if (m_touch1.phase == TouchPhase.Moved)
            {
                Vector2 a = Vector2.zero;
                m_initialDistance = Vector2.Distance(a, m_touch1.position);
                float prevDisBetwTouches = Vector2.Distance(a, m_touch1.position - m_touch1.deltaPosition);
                float Delta = m_initialDistance - prevDisBetwTouches;

                if (Mathf.Abs(Delta) > 0)
                {
                    Delta = 0.1f;
                }
                else
                {
                    m_initialDistance = Delta = 0;
                }

                _YRotation = Quaternion.Euler(0f, -m_touch1.deltaPosition.x * Delta, 0f);
                this.transform.rotation = _YRotation * this.transform.rotation;
            }
        }

        if (Input.touchCount == 2) // Проверка наличия двух касаний (щипка)
        {
            m_touch1 = Input.GetTouch(0);
            m_touch2 = Input.GetTouch(1);

            if (m_touch2.phase == TouchPhase.Began) // Если одно из касаний только началось
            {
                m_initialTouch1Pos = m_touch1.position;
                m_initialTouch2Pos = m_touch2.position;
                m_initialDistance = Vector2.Distance(m_initialTouch1Pos, m_initialTouch2Pos);
            }
            else if (m_touch1.phase == TouchPhase.Moved &&
                     m_touch2.phase == TouchPhase.Moved) // Если оба касания двигаются
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
