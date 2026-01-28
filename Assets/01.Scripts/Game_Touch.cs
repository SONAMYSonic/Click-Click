using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game_Touch : MonoBehaviour, IPointerDownHandler
{
    public GameManager GameManager;
    public Game_Upgrade Game_Upgrade;
    public Game_Item Game_Item;

    public void OnPointerDown(PointerEventData eventData)
    {
        // 아이템 배율 적용
        ulong gamePowerIncrease = GameManager.touch * Game_Item.GetGamePowerMultiplier();
        GameManager.gamePower += gamePowerIncrease;
        Debug.Log(GameManager.gamePower);
        GameManager.playerInfoUpdate();
    }
}
