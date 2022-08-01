using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeGame : MonoBehaviour
{
    DataManager.GameData dataManager;
    void Start()
    {
        if(GameObject.Find("DataManager") != null)
        {
            dataManager = GameObject.Find("DataManager").GetComponent<DataManager>().data;
            if (dataManager.volumeSitting)
            {
                gameObject.GetComponent<AudioSource>().volume = 0.2f;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().volume = 0f;
            }
        }
    }
}
