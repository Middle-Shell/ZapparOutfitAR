using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField] private GameObject m_target;

    public void SetTransformPosition()
    {
        m_target.transform.position = this.transform.position;
        m_target.transform.localScale = this.transform.localScale;
    }
}
