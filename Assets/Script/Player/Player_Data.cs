using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class Player_Data : ScriptableObject
{
    public float playerHealth;
    public float playerHealthDefault;

    public int playerLevel;
    [SerializeField] int playerDamageDefault;
    public int playerDamage;

    [SerializeField] int playerJumpStrenghtDefault;
    public int playerJumpStrenght;

    [SerializeField] float playerSpeedDefault;
    public float playerSpeed;

    [SerializeField] float playerDashStrenghtDefault;
    public float playerDashStrenght;

    void OnEnable()
    {
        playerHealth = playerHealthDefault;
        playerDamage = playerDamageDefault;
        playerSpeed = playerSpeedDefault;
        playerJumpStrenght = playerJumpStrenghtDefault;
        playerDashStrenght = playerDashStrenghtDefault;

    }

}
