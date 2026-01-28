using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Building_Buy : MonoBehaviour
{
    //시설 텍스트 표기용 스크립트
    
    public GameManager GameManager; //게임매니저 참조
    public Game_Upgrade Game_Upgrade; //업그레이드 스크립트 참조

    public TMP_Text SsalMuk_Text;   //쌀먹 텍스트
    public TMP_Text Macro_Text;     //매크로 텍스트

    public int SsalMuk_Level = 0;       //쌀먹 레벨
    public int Macro_Level = 0;         //매크로 레벨
    public ulong SsalMuk_Money = 0;     //쌀먹 분당 돈 증가량 > Game_Upgrade에서 관리
    public ulong Macro_GamePower = 0;   //매크로 분당 게임력 증가량 > Game_Upgrade에서 관리

    //쌀먹 텍스트 업데이트
    public void SsalMuk_TextUpdate()
    {
        SsalMuk_Text.text = "쌀 먹\n레벨: " + SsalMuk_Level + "\n분당 돈: " + SsalMuk_Money;
    }

    //매크로 텍스트 업데이트
    public void Macro_TextUpdate()
    {
        Macro_Text.text = "매크로\n레벨: " + Macro_Level + "\n분당 게임력: " + Macro_GamePower;
    }

    //쌀먹 첫 구매
    public void SsalMuk_Buy()
    {
        if (GameManager.isSsalMuk == true)
        {
            Debug.Log("Already bought SsalMuk!");
            return;
        }
        if (GameManager.gamePower >= 1000)
        {
            GameManager.gamePower -= 1000;
            GameManager.isSsalMuk = true;
            GameManager.autoMoney += 1;
            SsalMuk_Level += 1;
            StartCoroutine(GameManager.IncreaseMoney());
            SsalMuk_TextUpdate();
            GameManager.playerInfoList_clickUpdate();
            Game_Upgrade.updateUpgradeButtonTexts(1);
        }
        else
        {
            Debug.Log("Insufficient money for buying SsalMuk!");
        }
    }

    //매크로 첫 구매
    public void Macro_Buy()
    {
        if (GameManager.isMacro == true)
        {
            Debug.Log("Already bought Macro!");
            return;
        }
        if (GameManager.money >= 5)
        {
            GameManager.money -= 5;
            GameManager.isMacro = true;
            GameManager.autoGamePower += 1;
            Macro_Level += 1;
            StartCoroutine(GameManager.IncreaseGamePower());
            Macro_TextUpdate();
            GameManager.playerInfoList_clickUpdate();
            Game_Upgrade.updateUpgradeButtonTexts(2);
        }
        else
        {
            Debug.Log("Insufficient money for buying Macro!");
        }
    }
}
