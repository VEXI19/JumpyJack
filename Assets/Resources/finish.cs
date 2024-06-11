using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    public AudioSource finishsfx;
    public GameObject finishpanel;
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            finishsfx.GetComponent<AudioSource>().Play();

            Time.timeScale = 0;
            finishpanel.SetActive(true);
            
            if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("progressmanager", 1) && SceneManager.GetActiveScene().buildIndex < 3)
            {
                PlayerPrefs.SetInt("progressmanager", SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
