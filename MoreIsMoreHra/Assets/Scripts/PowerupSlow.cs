using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSlow : PowerupBase
{
    [SerializeField]
    private float powerupActiveTime = 1.5f;
    protected override IEnumerator Powerup()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(powerupActiveTime);
        Time.timeScale = 1f;
        gameManager.isPowerupActive = false;
    }
}
