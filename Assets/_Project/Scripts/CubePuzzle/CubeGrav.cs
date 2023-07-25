using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrav : MonoBehaviour
{
    [SerializeField]private Rigidbody _ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            Vector3 FollowDirection = (transform.position - _ball.position).normalized;
            _ball.AddForce(FollowDirection/2);
        
    }
}
