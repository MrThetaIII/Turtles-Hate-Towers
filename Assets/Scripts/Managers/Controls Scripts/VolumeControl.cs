using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Sprite soundOn;
    public Sprite soundOff;
    public bool volume;
    DataManager dataManager;
    Button volumeSetter;
    AudioSource musicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = Camera.main.GetComponent<AudioSource>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        volume = dataManager.data.volumeSitting;
        if (volume)
        {
            this.gameObject.GetComponent<Image>().sprite = soundOn;
            musicPlayer.volume = 0.5f;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = soundOff;
            musicPlayer.volume = 0f;
        }
        volumeSetter = this.GetComponent<Button>();
        volumeSetter.onClick.AddListener(ControlVolume);
    }

    // Update is called once per frame
    void ControlVolume()
    {
        volume = !volume;
        if (volume)
        {
            this.gameObject.GetComponent<Image>().sprite = soundOn;
            musicPlayer.volume = 0.5f;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = soundOff;
            musicPlayer.volume = 0f;
        }
        dataManager.data.volumeSitting = volume;
    }
}
