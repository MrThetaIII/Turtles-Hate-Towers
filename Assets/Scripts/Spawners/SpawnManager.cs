using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using PathCreation;

public class SpawnManager : MonoBehaviour
{
    public PathCreator[] spawner;
    public GameObject[] enemy;
    public List<ObjectPool<GameObject>> enemyPools = new List<ObjectPool<GameObject>>();
    ObjectPooler objectPooler;
    GameManager gameManager;
    int spawnIndx;
    int enemyIndx;
    public float spawnDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        foreach (GameObject enemyI in enemy)
        {
            enemyPools.Add(objectPooler.GetPool(enemyI, 20, 200));
        }
        Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        spawnIndx = Random.Range(0, spawner.Length);
        enemyIndx = Random.Range(0, enemy.Length);
        GameObject newEnemy = enemyPools[enemyIndx].Get();
        newEnemy.transform.SetPositionAndRotation(spawner[spawnIndx].path.GetPointAtDistance(0), spawner[spawnIndx].path.GetRotationAtDistance(0));
        newEnemy.GetComponent<EnemyControl>().spawnPath = spawner[spawnIndx];
        newEnemy.GetComponent<EnemyControl>().source = enemyPools[spawnIndx];
        if (gameManager.isGameActive)
        {
            Invoke("SpawnEnemy", spawnDelay);
        }
    }
}
