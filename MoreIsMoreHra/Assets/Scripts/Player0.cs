using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player0 : PlayerControllerBase
{
    protected override void SetPlayerHp()
    {
        gameManager.playerHp = 3;
    }
}
