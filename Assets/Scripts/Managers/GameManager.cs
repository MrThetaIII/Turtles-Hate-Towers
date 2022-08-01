using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button addSpeed, addFrequency, addRange, sellTower;
    public TextMeshProUGUI speedCost, rangeCost, frequincyCost, towerType, livesText, waveText, moneyText, noteficationArea, gameOverText;
    public Slider speedSlider, frequencySlider, rangeSlider, waveTimer;
    public GameObject infoPanel, towerBase, gameOverScreen, tutorialObject;

    public Projectile targetTower;
    DataManager dataManager = null;
    SpawnManager SpawnManager;

    AudioSource audioSource;
    public AudioClip gameOverAudio;

    public int lives = 30;
    public int money = 400;
    public int wave = 1;
    public int score = 0;

    int maxmumLevel = 10;
    int waveTime = 60;
    int currentTime = 0;

    public float difficultyConstant = 2f;
    public float waveSpeed = 2.5f;

    public bool isGameActive = true;

    void Start()
    {
        if (GameObject.Find("DataManager") != null)
        {
            dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
            int difficulty = dataManager.difficulty;
            switch (difficulty)
            {
                case 1:
                    maxmumLevel = 10;
                    difficultyConstant = 1.5f;
                    lives = 100;
                    money = 500;
                    waveTime = 120;
                    break;
                case 2:
                    maxmumLevel = 10;
                    difficultyConstant = 2f;
                    lives = 30;
                    money = 400;
                    waveTime = 60;
                    break;
                case 3:
                    maxmumLevel = 7;
                    difficultyConstant = 3f;
                    lives = 5;
                    money = 300;
                    waveTime = 30;
                    break;
            }
        }
        audioSource = Camera.main.GetComponent<AudioSource>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        addSpeed.onClick.AddListener(AddSpeed);
        addRange.onClick.AddListener(AddRange);
        addFrequency.onClick.AddListener(AddFrequency);
        sellTower.onClick.AddListener(SellTower);
        rangeSlider.maxValue = maxmumLevel;
        speedSlider.maxValue = maxmumLevel;
        frequencySlider.maxValue = maxmumLevel;
        UpdateMoney(0);
        UpdateLives(0);
        UpdateWave(0);
        waveTimer.maxValue = waveTime;
        StartCoroutine(WaveTimer());
        StartCoroutine(startTutorial());
    }



    //Tower Edit_________________________________________________
    public void AddSpeed()
    {
        if (targetTower.speedLevel < maxmumLevel)
        {
            if (money >= targetTower.speedCost)
            {
                UpdateMoney(-targetTower.speedCost);
                targetTower.speedLevel++;
                speedSlider.value++;
                targetTower.projectileSpeed += 5;
                targetTower.sellGain += targetTower.speedCost / 5;
                towerType.text = targetTower.type + " (" + targetTower.sellGain + "$)";
                if (targetTower.speedLevel == maxmumLevel)
                {
                    UpdateSpeedCost($"Max");
                }
                else
                {
                    targetTower.speedCost *= 2;
                    UpdateSpeedCost($"({targetTower.speedCost}$)");
                }
            }
            else
            {
                Notify($"You need at least {targetTower.speedCost}$ to upgrade speed");
            }

        }
    }
    public void AddFrequency()
    {
        if (targetTower.frequencyLevel < maxmumLevel)
        {
            if (money >= targetTower.frequencyCost)
            {
                UpdateMoney(-targetTower.frequencyCost);
                targetTower.frequencyLevel++;
                frequencySlider.value++;
                targetTower.firingDelay /= 1.5f;
                targetTower.sellGain += targetTower.frequencyCost / 5;
                towerType.text = targetTower.type + " (" + targetTower.sellGain + "$)";
                if (targetTower.frequencyLevel == maxmumLevel)
                {
                    UpdateFrequencyCost($"Max");
                }
                else
                {
                    targetTower.frequencyCost *= 2;
                    UpdateFrequencyCost($"({targetTower.frequencyCost}$)");
                }
            }
            else
            {
                Notify($"You need at least {targetTower.frequencyCost}$ to upgrade frequency");
            }
        }
    }
    public void AddRange()
    {
        if (targetTower.rangeLevel < maxmumLevel)
        {
            if (money >= targetTower.rangeCost)
            {
                UpdateMoney(-targetTower.rangeCost);
                targetTower.rangeLevel++;
                rangeSlider.value++;
                targetTower.firingDistance += 0.5f;
                targetTower.sellGain += targetTower.rangeCost / 5;
                towerType.text = targetTower.type + " (" + targetTower.sellGain + "$)";
                if (targetTower.rangeLevel == maxmumLevel)
                {
                    UpdateRangeCost($"Max");
                }
                else
                {
                    targetTower.rangeCost *= 2;
                    UpdateRangeCost($"({targetTower.rangeCost}$)");
                }
            }
            else
            {
                Notify($"You need at least {targetTower.rangeCost}$ to upgrade range");
            }
        }
    }

    //Score Edit_________________________________________________
    public void UpdateMoney(int amountToAdd)
    {
        money += amountToAdd;
        moneyText.text = money.ToString();
    }
    public void UpdateScore(int amountToAdd)
    {
        score += amountToAdd;
    }
    public void UpdateWave(int amountToAdd)
    {
        wave += amountToAdd;
        waveText.text = wave.ToString();
    }
    public void UpdateLives(int amountToAdd)
    {
        lives += amountToAdd;
        livesText.text = lives.ToString();
        if (lives == 0)
        {
            GameOver();
        }
    }
    public void UpdateSpeedCost(string cost)
    {
        speedCost.text = "Speed " + cost;
    }
    public void UpdateFrequencyCost(string cost)
    {
        frequincyCost.text = "Frequency " + cost;
    }
    public void UpdateRangeCost(string cost)
    {
        rangeCost.text = "Range " + cost;
    }

    //Basic______________________________________________________
    public void TargetTower(Projectile tower)
    {
        targetTower = tower;
        frequencySlider.value = tower.frequencyLevel;
        speedSlider.value = tower.speedLevel;
        rangeSlider.value = tower.rangeLevel;
        towerType.text = tower.type + " (" + tower.sellGain + "$)";
        if (tower.rangeLevel < maxmumLevel) { UpdateRangeCost($"({tower.rangeCost}$)"); } else { UpdateRangeCost("(Max)"); }
        if (tower.frequencyLevel < maxmumLevel) { UpdateFrequencyCost($"({tower.frequencyCost}$)"); } else { UpdateFrequencyCost("(Max)"); }
        if (tower.speedLevel < maxmumLevel) { UpdateSpeedCost($"({tower.speedCost}$)"); } else { UpdateSpeedCost("(Max)"); }
    }

    public void SellTower()
    {
        Vector3 position = targetTower.gameObject.transform.position - new Vector3(0.0f, 1.0f, 0.0f);
        UpdateMoney(targetTower.sellGain);
        Destroy(targetTower.gameObject);
        targetTower = null;
        infoPanel.SetActive(false);
        Instantiate(towerBase, position, towerBase.transform.rotation);
    }

    public void Notify(string message)
    {
        StartCoroutine(NotifyRoutein(message));
    }
    IEnumerator NotifyRoutein(string message)
    {
        noteficationArea.text = message;
        noteficationArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        noteficationArea.gameObject.SetActive(false);
    }

    IEnumerator WaveTimer()
    {
        while (isGameActive)
        {
            while(currentTime < waveTime)
            {
                yield return new WaitForSeconds(1);
                currentTime += 1;
                waveTimer.value = currentTime;
            }
            
            UpdateWave(1);
            waveSpeed = wave * difficultyConstant;
            SpawnManager.spawnDelay = 2.5f / (wave * difficultyConstant);
            currentTime = 0;
        }
    }

    IEnumerator startTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        tutorialObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        tutorialObject.SetActive(false);
    }
    void GameOver()
    {
        isGameActive = false;
        addFrequency.onClick.RemoveAllListeners();
        addRange.onClick.RemoveAllListeners();
        addSpeed.onClick.RemoveAllListeners();
        sellTower.onClick.RemoveAllListeners();
        gameOverScreen.SetActive(true);
        audioSource.PlayOneShot(gameOverAudio);
        if (GameObject.Find("DataManager") != null)
        {
            int highScore = dataManager.data.score;
            if(score >= highScore)
            {
                gameOverText.text = $"Congratulations {dataManager.user}\nNew Record: {score}";
                dataManager.data.score = score;
                dataManager.data.nameHolder = dataManager.user;
                dataManager.data.newGame = false;
                dataManager.SaveData(dataManager.data, dataManager.fileName);
            }
            else
            {
                gameOverText.text = $"{dataManager.user}'s Score : {score}\nHighest score by {dataManager.data.nameHolder}: {dataManager.data.score}";
            }
        }
        else
        {
            gameOverText.text = $"You opened from the Editor, you sneaky boy!\nScore: {score}";
        }
    }

}
