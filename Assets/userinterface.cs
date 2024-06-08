using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class userinterface : MonoBehaviour
{
    public GameObject panel;
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
        if (panel != null && Input.GetKey(KeyCode.P))
        {
            this.pause();
            panel.gameObject.SetActive(true);
        }
    }

    public void resetprogress()
    {
        PlayerPrefs.DeleteAll();
        GameObject.FindGameObjectWithTag("ProgressManager").GetComponent<progressmanager>().Start();
    }
}
