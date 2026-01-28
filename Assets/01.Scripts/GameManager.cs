using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public Game_Item Game_Item; //게임 아이템 참조

    public ulong gamePower;      //플레이어 게임력
    public ulong gamePower2;    //ulong 넘기면 사용
    public ulong money;
    public ulong money2;        //동일
    public int burger;
    public ulong burger2;       //동일
    public TMP_Text Info;     //상단 플레이어 능력치 표시

    public ulong touch = 1;       //터치당 증가량
    public ulong autoMoney = 0;     //분당 돈 증가량
    public ulong autoGamePower = 0; //자동으로 증가하는 게임력

    public Boolean isSsalMuk = false; // 쌀먹 구매 여부
    public Boolean isMacro = false;   // 매크로 구매 여부

    public bool isIncreaseGamePowerRunning = false; // 게임력 증가 코루틴 실행 여부
    public bool isIncreaseMoneyRunning = false;      // 돈 증가 코루틴 실행 여부

    private void Start()
    {
        if (isSsalMuk == true)
        {
            StartCoroutine(IncreaseMoney());
        }
        
    }

    //하단 메뉴 플레이어 능력치 리스트
    public List<TMP_Text> infoList;

    //플레이어 표시 능력치 업데이트
    public void playerInfoUpdate()
    {
        Info.text = "게임력: " + gamePower.ToString() + "\n" + "돈: " + money.ToString() 
            + "\n" + "버거: " + burger.ToString();
    }

    // 1분에 한번씩 자동으로 돈 증가
    public IEnumerator IncreaseMoney()
    {
        if (isIncreaseMoneyRunning == true)
        {
            yield break;
        }
        isIncreaseMoneyRunning = true;         // 코루틴 실행 중
        while (true)
        {
            yield return new WaitForSeconds(60); // 60초 대기
            money += (ulong)autoMoney;            // 돈 증가
            playerInfoUpdate();                 // 플레이어 표시 능력치 업데이트
        }
    }

    // 1분에 한번씩 자동으로 게임력 증가
    public IEnumerator IncreaseGamePower()
    {
        if (isIncreaseGamePowerRunning == true)
        {
            yield break;
        }
        isIncreaseGamePowerRunning = true;      // 코루틴 실행 중
        while (true)
        {
            yield return new WaitForSeconds(60);    // 60초 대기
            gamePower += autoGamePower * Game_Item.GetGamePowerMultiplier(); // 게임력 증가
            playerInfoUpdate();                     // 플레이어 표시 능력치 업데이트
        }
    }

    //INFO 탭 UI 업데이트 
    public void playerInfoList_clickUpdate()
    {
        infoList[0].text = "클릭 당 증가 : " + touch.ToString();
        infoList[1].text = "분당 게임력 자동 증가량 : " + autoGamePower.ToString();
        infoList[2].text = "분당 돈 자동 증가량 : " + autoMoney.ToString();
    }
    
    
}
