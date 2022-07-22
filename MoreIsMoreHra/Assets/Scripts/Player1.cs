using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerControllerBase
{
    protected override void SetPlayerHp()
    {
        gameManager.playerHp = 5;
    }
}
