using UnityEngine;
using UnityEngine.UI;

public class sliderinit : MonoBehaviour
{
    public int mode;
    // Start is called before the first frame update
    void Start()
    {   
        if (mode == 0)
            this.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("volume", 500) / 1000f;
        else
            this.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("effects", 500) / 1000f;
    }
}
