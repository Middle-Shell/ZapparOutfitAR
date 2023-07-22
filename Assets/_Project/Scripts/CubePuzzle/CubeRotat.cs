using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
    
}
