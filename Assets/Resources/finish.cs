using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    public GameObject panel;

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            panel.gameObject.SetActive(true);
            
            if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("progressmanager", 1) && SceneManager.GetActiveScene().buildIndex < 3)
            {
                PlayerPrefs.SetInt("progressmanager", SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
