using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using PathCreation;

public class EnemyControl : MonoBehaviour
{
    public PathCreator spawnPath;
    GameManager gameManager;
    public ObjectPool<GameObject> source;
    public float speed = 2.5f;
    public float distanceTraveled = 0;
    public bool isTargeted = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (distanceTraveled < spawnPath.path.length)
            {
                transform.position = spawnPath.path.GetPointAtDistance(distanceTraveled);
                transform.rotation = spawnPath.path.GetRotationAtDistance(distanceTraveled);
                distanceTraveled += speed * Time.deltaTime;
            }
            else
            {
                source.Release(gameObject);
                gameManager.UpdateLives(-1);
            }
        }
    }
}

