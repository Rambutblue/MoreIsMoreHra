using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPauseSpawn : PowerupBase
{
    [SerializeField]
    private float powerupActiveTime = 0.75f;
    protected override void Powerup()
    {
        gameManager.spawnTime = -powerupActiveTime;
        gameManager.InactivateAllObstacles();
        gameManager.isPowerupActive = false;
        isPowerupBoostActive = false;
    }
}
