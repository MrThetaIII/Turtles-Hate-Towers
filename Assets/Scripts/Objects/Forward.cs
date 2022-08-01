using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Forward : MonoBehaviour
{
    public float projectileSpeed = 50;
    public GameObject target;
    public GameManager gameManager;
    public ObjectPool<GameObject> source;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (target.activeInHierarchy)
            {
                transform.LookAt(target.transform);
                transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
            }
            else
            {
                source.Release(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            source.Release(gameObject);
            target.GetComponent<EnemyControl>().source.Release(other.gameObject);
            gameManager.UpdateMoney(10);
            gameManager.UpdateScore(1);
        }
    }
}
