using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    private Quaternion CurNewRot;
    public int numberOfObjects = 1;

    void Start()
    {
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform hostTransform = transform;
            Vector3 hostSize = new Vector3(3f,3f,3f); 
//---------------------------------------------------------------------------------------
            float randomX = Random.Range(-hostSize.x, hostSize.x );
            float randomY = Random.Range(-hostSize.y, hostSize.y );

            Vector3 spawnPosition = hostTransform.position + new Vector3(randomX, randomY, 0f);
//---------------------------------------------------------------------------------------
            CurNewRot = objectToPlace.transform.rotation;
            Quaternion CurNewRot2 = Quaternion.Euler(90f,CurNewRot.eulerAngles.y,CurNewRot.eulerAngles.z);
            objectToPlace.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
//---------------------------------------------------------------------------------------
   
            Instantiate(objectToPlace, spawnPosition, CurNewRot2);
        }
    }
}
