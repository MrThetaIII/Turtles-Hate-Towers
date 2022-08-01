using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    //Objects
    public GameObject spawnOriginObj;
    public GameObject projectile;
    public GameObject infoPanel;
    public ObjectPooler objectPooler;
    public ObjectPool<GameObject> projectilesPool;


    GameManager gameManager;

    //Object Place Holder
    GameObject[] targetsList;

    //Variable Place Holders
    Vector3 spawnOrigin;
    float spawnOffset = 0.5f;

    //Variables
    public float firingDelay = 1;
    public float firingDistance = 3.5f;
    public float projectileSpeed = 5;
    bool isClicked = false;

    public string type = "Arrow Tower";
    public int speedLevel = 1;
    public int frequencyLevel = 1;
    public int rangeLevel = 1;
    public int speedCost = 200;
    public int rangeCost = 200;
    public int frequencyCost = 200;
    public int sellGain = 50;
    public int BuildingCoast = 100;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        infoPanel = gameManager.infoPanel;
        projectilesPool = objectPooler.GetPool(projectile, 5, 20);
        spawnOrigin = spawnOriginObj.transform.position;
        Focus();
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(firingDelay);
            targetsList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in targetsList)
            {
                if (!target.GetComponent<EnemyControl>().isTargeted && Vector3.Distance(target.transform.position, transform.position) < firingDistance)
                {
                    spawnOriginObj.transform.LookAt(target.transform.position, Vector3.up);
                    GameObject proj = projectilesPool.Get();
                    proj.transform.SetPositionAndRotation(spawnOrigin + spawnOriginObj.transform.forward * spawnOffset, projectile.transform.rotation);
                    proj.GetComponent<Forward>().target = target;
                    proj.GetComponent<Forward>().source = projectilesPool;
                    proj.GetComponent<Forward>().projectileSpeed = projectileSpeed;
                    target.GetComponent<EnemyControl>().isTargeted = true;
                    break;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Focus();
        }
    }

    public void Focus()
    {
        if (gameManager.targetTower == this)
        {
            if (isClicked)
            {
                infoPanel.SetActive(false);
                isClicked = false;
            }
            else
            {
                infoPanel.SetActive(true);
                isClicked = true;
            }
        }
        else
        {
            infoPanel.SetActive(true);
            gameManager.TargetTower(this);
            isClicked = true;
        }
    }
}
