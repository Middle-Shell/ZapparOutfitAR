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
            var localScale = hostTransform.localScale;
            Vector3 hostSize = new Vector3(localScale.x, localScale.y,
                localScale.z);//new Vector3(3f,3f,3f);
//---------------------------------------------------------------------------------------
            float randomX = Random.Range(-hostSize.x, hostSize.x );
            float randomY = Random.Range(-hostSize.y, hostSize.y );

            Vector3 spawnPosition = hostTransform.position + new Vector3(randomX, randomY, 0f);
//---------------------------------------------------------------------------------------
            CurNewRot = objectToPlace.transform.rotation;
            var rotation = hostTransform.rotation;
            print(rotation.x);//чтобы сделать угол в 90, для вычисления надо конвертировать rotation.x в эйлера
            Quaternion curNewRot2 = Quaternion.Euler(rotation.x - (90 - rotation.x), CurNewRot.eulerAngles.y,
                CurNewRot.eulerAngles.z);
            objectToPlace.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
//---------------------------------------------------------------------------------------
   
            Instantiate(objectToPlace, spawnPosition, curNewRot2, hostTransform);
        }
    }
}
