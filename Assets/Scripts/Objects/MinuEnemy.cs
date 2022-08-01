using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MinuEnemy : MonoBehaviour
{
    public PathCreator path;
    float speed = 2;
    float distanceTraveled = 0;
    void Update()
    {
        if(distanceTraveled < path.path.length)
        {
            transform.position = path.path.GetPointAtDistance(distanceTraveled);
            transform.rotation = path.path.GetRotationAtDistance(distanceTraveled);
            distanceTraveled += speed * Time.deltaTime;
        }
        else
        {
            distanceTraveled = 0;
        }
    }
}
