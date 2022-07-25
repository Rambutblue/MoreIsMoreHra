using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPauseSpawn : PowerupBase
{
    [SerializeField]
    private float powerupActiveTime = 1.5f;
    protected override IEnumerator Powerup()
    {
        gameManager.spawnTime = -powerupActiveTime;
        yield return new WaitForSeconds(powerupActiveTime);
        gameManager.isPowerupActive = false;
    }
}
