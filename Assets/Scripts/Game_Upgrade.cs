using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Upgrade : MonoBehaviour
{
    public GameManager GameManager;                         //게임매니저 참조
    public Game_Building_Buy Game_Building_Buy;             //건물 구매 스크립트 참조

    public ulong touchInitialUpgradeCost = 2;                 // 클릭 업그레이드에 필요한 초기 게임력
    public float touchUpgradeCostIncreaseRate = 1.3f;       // 클릭 업그레이드에 필요한 게임력의 증가 비율
    private int currentTouchInitialUpgradeLevel = 1;            // 현재 업그레이드 레벨

    public ulong SsalMukUpgradeCost = 10;                 // 쌀먹 업그레이드에 필요한 돈
    public float SsalMukUpgradeCostIncreaseRate = 1.7f;   // 쌀먹 업그레이드에 필요한 돈의 증가 비율
    public float SsalMukUpgradeRate = 1.3f;             // 쌀먹 업그레이드시 획득 돈 증가 비율

    public ulong MacroUpgradeCost = 300;                   // 매크로 업그레이드에 필요한 게임력
    public float MacroUpgradeCostIncreaseRate = 1.5f;    // 매크로 업그레이드에 필요한 게임력의 증가 비율
    public float MacroUpgradeRate = 1.4f;                // 매크로 업그레이드시 획득 게임력 증가 비율

    public List<GameObject> upgradeButtons;                 // 업그레이드 버튼들

    // 다음 업그레이드에 필요한 게임력
    private ulong nextUpgradeCost;

    void Start()
    {

    }

    // 업그레이드 버튼의 텍스트 업데이트
    public void updateUpgradeButtonTexts(int x)
    {
        switch (x)
        {
            case 0:
                upgradeButtons[0].GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
                    "클릭 수치 업그레이드\n필요 게임력: " + nextUpgradeCost;
                break;
            case 1:
                upgradeButtons[1].GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
                    "쌀먹 업그레이드\n현재 레벨: " + Game_Building_Buy.SsalMuk_Level + "\n필요 돈: " + SsalMukUpgradeCost;
                break;
            case 2:
                upgradeButtons[2].GetComponentInChildren<TMPro.TextMeshProUGUI>().text =
                    "매크로 업그레이드\n현재 레벨: " + Game_Building_Buy.Macro_Level + "\n필요 게임력: " + MacroUpgradeCost;
                break;
        }
    }

    // 다음 업그레이드에 필요한 게임력을 계산하는 함수
    private void CalculateNextUpgradeCost()
    {
        float upgradeCost = touchInitialUpgradeCost * Mathf.Pow(touchUpgradeCostIncreaseRate, currentTouchInitialUpgradeLevel);
        nextUpgradeCost = (ulong)System.Math.Ceiling(upgradeCost); // double 값을 ulong로 형변환하여 올림
    }

    // 터치 업그레이드를 수행하는 함수
    public void touchInitailUpgrade()
    {
        // 게임력이 충분한지 확인하고 업그레이드 수행
        if (GameManager.gamePower >= nextUpgradeCost)
        {
            // 게임력 소모
            GameManager.gamePower -= nextUpgradeCost;

            // 업그레이드 레벨 증가
            currentTouchInitialUpgradeLevel++;
            // 터치 게임력 증가
            GameManager.touch += 1;
            // 다음 업그레이드에 필요한 게임력 재계산
            CalculateNextUpgradeCost();
            //현재 게임력 수치 업데이트
            GameManager.playerInfoUpdate();
            //INFO 탭 UI 업데이트 
            GameManager.playerInfoList_clickUpdate();
            // 업그레이드 버튼의 텍스트 업데이트
            upgradeButtons[0].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "클릭 수치 업그레이드\n필요 게임력: " + nextUpgradeCost;

            Debug.Log("Upgrade successful! Next upgrade cost: " + nextUpgradeCost);
        }
        else
        {
            Debug.Log("Insufficient game power for upgrade!");
        }
    }

    // 쌀먹 업그레이드를 수행하는 함수
    public void SsalMukUpgrade()
    {
        // 쌀먹 레벨이 0이면 구매를 먼저 해야함
        if (Game_Building_Buy.SsalMuk_Level == 0)
        {
            Debug.Log("You need to buy SsalMuk first!");
            return;
        }
        // 돈이 충분한지 확인하고 업그레이드 수행
        if (GameManager.money >= SsalMukUpgradeCost)
        {
            // 돈 소모
            GameManager.money -= SsalMukUpgradeCost;

            // 쌀먹 레벨 증가
            Game_Building_Buy.SsalMuk_Level += 1;
            // 쌀먹 분당 돈 증가 (업그레이드 기준은 레벨)
            Game_Building_Buy.SsalMuk_Money += 
                (ulong)System.Math.Ceiling(Game_Building_Buy.SsalMuk_Level * SsalMukUpgradeRate);
            // double 값을 ulong로 형변환하여 올림

            // 업그레이드 된 돈 autoMoney에 합산
            GameManager.autoMoney += Game_Building_Buy.SsalMuk_Money;
            // 다음 업그레이드에 필요한 돈 재계산
            SsalMukUpgradeCost = (ulong)System.Math.Ceiling(SsalMukUpgradeCost * SsalMukUpgradeCostIncreaseRate); 
            // double 값을 ulong로 형변환하여 올림

            //현재 게임력 수치 업데이트
            GameManager.playerInfoUpdate();
            //INFO 탭 UI 업데이트 
            GameManager.playerInfoList_clickUpdate();
            // 업그레이드 버튼의 텍스트 업데이트
            upgradeButtons[1].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = 
                "쌀먹 업그레이드\n현재 레벨: " + Game_Building_Buy.SsalMuk_Level + "\n필요 돈: " 
                + SsalMukUpgradeCost;

            Debug.Log("Upgrade successful! Next upgrade cost: " + SsalMukUpgradeCost);
        }
        else
        {
            Debug.Log("Insufficient money for upgrade!");
        }
    }

    // 매크로 업그레이드를 수행하는 함수
    public void MacroUpgrade()
    {
        // 매크로 레벨이 0이면 구매를 먼저 해야함
        if (Game_Building_Buy.Macro_Level == 0)
        {
            Debug.Log("You need to buy Macro first!");
            return;
        }
        // 게임력이 충분한지 확인하고 업그레이드 수행
        if (GameManager.gamePower >= MacroUpgradeCost)
        {
            // 게임력 소모
            GameManager.gamePower -= MacroUpgradeCost;

            // 매크로 레벨 증가
            Game_Building_Buy.Macro_Level += 1;
            // 매크로 분당 게임력 증가 (업그레이드 기준은 레벨)
            Game_Building_Buy.Macro_GamePower += 
                (ulong)System.Math.Ceiling(Game_Building_Buy.Macro_Level * MacroUpgradeRate); 
            // double 값을 ulong로 형변환하여 올림

            // 업그레이드 된 게임력 autoGamePower에 합산
            GameManager.autoGamePower += Game_Building_Buy.Macro_GamePower;
            // 다음 업그레이드에 필요한 게임력 재계산
            MacroUpgradeCost = (ulong)System.Math.Ceiling(MacroUpgradeCost * MacroUpgradeCostIncreaseRate); 
            // double 값을 ulong로 형변환하여 올림

            //현재 게임력 수치 업데이트
            GameManager.playerInfoUpdate();
            //INFO 탭 UI 업데이트 
            GameManager.playerInfoList_clickUpdate();
            // 업그레이드 버튼의 텍스트 업데이트
            upgradeButtons[2].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = 
                "매크로 업그레이드\n현재 레벨: " + Game_Building_Buy.Macro_Level + "\n필요 게임력: " 
                + MacroUpgradeCost;

            Debug.Log("Upgrade successful! Next upgrade cost: " + MacroUpgradeCost);
        }
        else
        {
            Debug.Log("Insufficient game power for upgrade!");
        }
    }
}
