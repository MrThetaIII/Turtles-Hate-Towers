using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    DataManager.GameData dataManager;
    public GameObject EnterScreen;
    public GameObject EnterControls;
    private void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>().data;
        if (!dataManager.newGame)
        {
            highScoreText.text = $"Current High Score Holder is <{dataManager.nameHolder}>\nWho Killed {dataManager.score} enemies";
        }
        else
        {
            highScoreText.text = "Brand New Game\nWe Are Writing History Here";
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Return()
    {
        EnterScreen.SetActive(false);
        EnterControls.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
