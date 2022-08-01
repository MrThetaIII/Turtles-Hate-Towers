using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputReciever : MonoBehaviour
{
    TMP_InputField nameField;
    DataManager dataManager;
    public GameObject enterControls;
    public TextMeshProUGUI welcomeText;
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        nameField = this.GetComponent<TMP_InputField>();
        nameField.onEndEdit.AddListener(NameSet);
    }

    // Update is called once per frame
    void NameSet(string name)
    {
        if(name.Length > 0)
        {
            enterControls.SetActive(true);
            welcomeText.text = $"Welcome {name}";
            dataManager.user = name;
        }
    }
}
