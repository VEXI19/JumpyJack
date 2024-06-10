using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode down;
    public KeyCode teleportToCheckpoint;
    public KeyCode saveCheckpoint;
    public KeyCode hologram;
    public KeyCode pause;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the object persistent between scenes

            left = (KeyCode)PlayerPrefs.GetInt("left 97", (int)KeyCode.A);
            right = (KeyCode)PlayerPrefs.GetInt("right100", (int)KeyCode.D);
            jump = (KeyCode)PlayerPrefs.GetInt("jump 32", (int)KeyCode.Space);
            down = (KeyCode)PlayerPrefs.GetInt("down115", (int)KeyCode.S);
            teleportToCheckpoint = (KeyCode)PlayerPrefs.GetInt("teleport116", (int)KeyCode.T);
            saveCheckpoint = (KeyCode)PlayerPrefs.GetInt("save115", (int)KeyCode.S);
            hologram = (KeyCode)PlayerPrefs.GetInt("hologram103", (int)KeyCode.G);
            pause = (KeyCode)PlayerPrefs.GetInt("pause112", (int)KeyCode.P);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void assign(string name, KeyCode kcode)
    {
        switch (name)
        {
            case "right100":
                right = kcode;
                break;
            case "left 97":
                left = kcode;
                break;
            case "jump 32":
                jump = kcode;
                break;
            case "down115":
                down = kcode;
                break;
            case "teleport116":
                teleportToCheckpoint = kcode;
                break;
            case "save115":
                saveCheckpoint = kcode;
                break;
            case "hologram103":
                hologram = kcode;
                break;
            case "pause112":
                pause = kcode;
                break;
        }
    }

    private float speed;
    private float gravity = 0f;
    private float acceleration = 3f;
    private float deceleration = 3f;
    public void Update()
    {
        if (Input.GetKey(left) && Input.GetKey(right))
        {
            speed = 0f;
            return;
        }
        else if (Input.GetKey(left))
        {
            speed = -1f;

            if (gravity > 0f)
            {
                gravity = 0f;
                return;
            }
        }
        else if (Input.GetKey(right))
        {
            speed = 1f;

            if (gravity < 0f)
            {
                gravity = 0f;
                return;
            }
        }
        else
        {
            speed = 0f;
        }

        if (speed == 0)
            gravity = Mathf.MoveTowards(gravity, 0f, Time.deltaTime * deceleration);
        else
            gravity = Mathf.MoveTowards(gravity, speed, Time.deltaTime * acceleration);

        gravity = Mathf.Clamp(gravity, -1, 1);
    }

    public float GetAxisRaw(string name)
    {
        return speed;
    }

    public float GetAxis(string name)
    {
        return gravity;
    }
}
