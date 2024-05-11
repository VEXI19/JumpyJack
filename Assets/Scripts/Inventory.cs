using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance = new();

    [SerializeField] private bool _hasDoubleJump = false;
    [SerializeField] private bool _hasClimbing = false;
    [SerializeField] private bool _hasUmbrella = false;
    [SerializeField] private bool _hasHologram = false;

    public enum PowerUps
    {
        DOUBLE_JUMP,
        CLIMBING,
        UMBRELLA,
        HOLOGRAM
    }

    public bool HasDoubleJump
    {
        get => _hasDoubleJump;
    }

    public bool HasClimbing
    {
        get => _hasClimbing;
    }

    public bool HasUmbrella
    {
        get => _hasUmbrella;
    }

    public bool HasHologram
    {
        get => _hasHologram;
    }
    public void AddPowerUp(PowerUps powerUp)
    {
        switch (powerUp)
        {
            case (PowerUps.DOUBLE_JUMP):
                {
                    _hasDoubleJump = true;
                    break;
                }
            case (PowerUps.CLIMBING):
                {
                    _hasClimbing = true;
                    break;
                }
            case (PowerUps.HOLOGRAM):
                {
                    _hasHologram = true;
                    break;
                }
            case (PowerUps.UMBRELLA):
                {
                    _hasUmbrella = true;
                    break;
                }
        }
    }
}
