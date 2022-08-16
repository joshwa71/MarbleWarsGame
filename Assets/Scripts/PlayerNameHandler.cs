using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerNameHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerNickname"))
        {
            inputField.text = PlayerPrefs.GetString("PlayerNickname");
            Debug.Log("Set text Input Field");
        }
    }

    public void OnSetNameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", inputField.text);
        PlayerPrefs.Save();
        Debug.Log($"Save nick name as input field: {inputField.text}");
    }
    

}
