using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // 디버그용 매니저
    public GameManager GameManager; //게임매니저 참조
    
    public void DebugGamePower()
    {
        GameManager.gamePower += 1000;
        GameManager.money += 1000;
    }
}
