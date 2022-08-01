using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public int difficulty;
    Button setDifficultyButton;
    public GameObject EnterGameScreen;
    DataManager dataManager;
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        setDifficultyButton = this.GetComponent<Button>();
        setDifficultyButton.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void SetDifficulty()
    {
        dataManager.difficulty = difficulty;
        EnterGameScreen.SetActive(true);
    }
}
