using UnityEngine;

public class setvolume : MonoBehaviour
{
    public int mode;
    // Start is called before the first frame update
    void Start()
    {
        if (mode == 0)
            this.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("volume", 500) / 1000f;
        else
            this.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("effects", 500) / 1000f;
    }
}
