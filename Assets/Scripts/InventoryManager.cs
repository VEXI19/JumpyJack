
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    InventoryPowerUps powerUps;
    InventoryPowerUps powerUpsSave;
    public bool _hologramUsed = false;
    public GameObject holo;
    public GameObject holoObject;
    public AudioSource hologramsfx;

    struct InventoryPowerUps
    {
        public bool _doubleJump;
        public bool _climbing;
        public bool _umbrella;
        public bool _hologram;
    }

    private void Start()
    {
        powerUps._climbing = false;
        powerUps._doubleJump = false;
        powerUps._climbing = false;
        powerUps._hologram = false;
    }

    public void Save()
    {
        powerUpsSave = powerUps;
    }

    public void Restore()
    {
        powerUps = powerUpsSave;
    }

    public bool DoubleJump { get => powerUps._doubleJump; }

    public bool Climbing { get => powerUps._climbing; }

    public bool Umbrella { get => powerUps._umbrella; }

    public bool Hologram { get => powerUps._hologram; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
        {
            powerUps._doubleJump = true;
        }
        if (collision.gameObject.CompareTag("ClimbingPowerUp"))
        {
            powerUps._climbing = true;
        }
        if (collision.gameObject.CompareTag("UmbrellaPowerUp"))
        {
            powerUps._umbrella = true;
        }
        if (collision.gameObject.CompareTag("HologramPowerUp"))
        {
            powerUps._hologram = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.hologram) && powerUps._hologram)
        {
            hologramsfx.GetComponent<AudioSource>().Play();

            if (!_hologramUsed)
            {
                holo = Instantiate(holoObject, position: this.transform.position + new Vector3(0, 0, 0.5f), this.transform.rotation);
                if (!GetComponent<CharacterMovement>().IsFacingRight)
                {
                    Vector2 localScale = holo.transform.localScale;
                    localScale.x *= -1f;
                    holo.transform.localScale = localScale;
                }
                _hologramUsed = true;
            }

            else
            {
                Destroy(holo);
                _hologramUsed = false;
            }


        }

        if (_hologramUsed)
        {
            float distance = Vector3.Distance(holo.transform.position, transform.position);
            if (distance > 10f)
            {
                hologramsfx.GetComponent<AudioSource>().Play();
                Destroy(holo);
                _hologramUsed = false;
            }
        }

       

    }

}
 