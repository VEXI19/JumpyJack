using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class keybindsmanager : MonoBehaviour
{
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = Enum.GetName(typeof(KeyCode), PlayerPrefs.GetInt(button.name, Convert.ToInt32(button.name.Substring(button.name.Length - 3))));
        }
    }

    public void resetkeybinds()
    {
        foreach (var button in buttons)
        {
            PlayerPrefs.DeleteKey(button.name);
            button.GetComponentInChildren<TextMeshProUGUI>().text = Enum.GetName(typeof(KeyCode), PlayerPrefs.GetInt(button.name, Convert.ToInt32(button.name.Substring(button.name.Length - 3))));
            InputManager.Instance.assign(button.name, (KeyCode)Convert.ToInt32(button.name.Substring(button.name.Length - 3)));
        }
    }
}
