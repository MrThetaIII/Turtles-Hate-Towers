using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject tower;
    public Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            int cost = tower.GetComponent<Projectile>().BuildingCoast;
            if (gameManager.money >= cost)
            {
                Instantiate(tower, transform.position + offset, tower.transform.rotation);
                gameManager.UpdateMoney(-cost);
                Destroy(gameObject);
            }
            else
            {
                gameManager.Notify($"You need at least {cost}$ to build this tower");
            }
        }
    }
}
