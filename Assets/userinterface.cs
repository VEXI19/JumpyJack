using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class userinterface : MonoBehaviour
{
    public GameObject pausepanel;
    public void exit()
    {
        Application.Quit();
    }

    public void play(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void pause()
    {
        Time.timeScale = 0;
    }

    public void resume()
    {
        Time.timeScale = 1;
    }

    public void Update()
    {
        if (pausepanel != null && Input.GetKey(InputManager.Instance.pause))
        {
            this.pause();
            pausepanel.SetActive(true);
        }
    }

    public void resetprogress()
    {
        PlayerPrefs.DeleteKey("progressmanager");
        GameObject.FindGameObjectWithTag("ProgressManager").GetComponent<progressmanager>().Start();
    }

    public void assignkey(Button button)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "";

        StartCoroutine(waitForKeyPress(button));
    }

    private IEnumerator waitForKeyPress(Button button)
    {
        while (!Input.anyKey)
        {
            yield return null;
        }

        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                if (kcode == KeyCode.Escape) break;

                PlayerPrefs.SetInt(button.name, (int)kcode);
                InputManager.Instance.assign(button.name, kcode);
                break;
            }
        }

        button.GetComponentInChildren<TextMeshProUGUI>().text = Enum.GetName(typeof(KeyCode), PlayerPrefs.GetInt(button.name, Convert.ToInt32(button.name.Substring(button.name.Length - 3))));
    }

    public void setvolume(Slider slider)
    {
        if (slider.name == "musicslider")
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = slider.value;
            PlayerPrefs.SetInt("volume", Convert.ToInt32(slider.value * 1000));
        }
        else
        {
            PlayerPrefs.SetInt("effects", Convert.ToInt32(slider.value * 1000));
        }
    }
}
