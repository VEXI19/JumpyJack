using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressmanager : MonoBehaviour
{
    public Button[] buttons;
    public void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("progressmanager", 1); i++)
        {
            buttons[i].interactable = true;
        }
    }
}
