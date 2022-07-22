using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerControllerBase
{
    protected override void SetPlayerHp()
    {
        gameManager.playerHp = 10;
    }
}
