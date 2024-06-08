using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public KeyCode walkRight = KeyCode.D;
    public KeyCode walksLeft = KeyCode.A;
    public KeyCode jump = KeyCode.Space;
    public KeyCode down = KeyCode.S;
    public KeyCode teleportToCheckpoint = KeyCode.T;
    public KeyCode saveCheckpoint = KeyCode.S;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the object persistent between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
