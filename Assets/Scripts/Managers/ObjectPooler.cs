using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    GameObject OnCreated(GameObject objectToCreate)
    {
        GameObject instance = Instantiate(objectToCreate);
        instance.SetActive(false);
        return instance;
    }

    void OnRequested(GameObject objectToRequest)
    {
        objectToRequest.SetActive(true);
        if (objectToRequest.CompareTag("Enemy"))
        {
            objectToRequest.GetComponent<EnemyControl>().speed = gameManager.waveSpeed;
        }
    }

    void OnReleased(GameObject objectToRelease)
    {
        objectToRelease.SetActive(false);
        if (objectToRelease.CompareTag("Enemy"))
        {
            objectToRelease.GetComponent<EnemyControl>().distanceTraveled = 0;
            objectToRelease.GetComponent<EnemyControl>().isTargeted = false;
        }
    }

    void OnDisposed(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    public ObjectPool<GameObject> GetPool(GameObject instance, int defaultSize, int maxSize)
    {
        return new ObjectPool<GameObject>(createFunc: () => OnCreated(instance), actionOnGet: (obj) => OnRequested(obj),
            actionOnRelease: (obj) => OnReleased(obj), actionOnDestroy: (obj) => OnDisposed(obj), collectionCheck: false, defaultCapacity: defaultSize, maxSize: maxSize);
    }
}
