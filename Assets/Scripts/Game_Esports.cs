using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class E_Sports
{
    public string Name;
    public ulong winPower;

    public E_Sports() { }

    public E_Sports(string name, ulong winpower)
    {
        Name = name;
        winPower = winpower;
    }
}

public class Game_Esports : MonoBehaviour
{
    //게임력을 기반으로 승패를 결정하는 스크립트
    public GameManager GameManager;    
    public E_Sports[] E_sports_Array;


    public void E_Sports_Battle(int x)
    {
        ulong playerPower = GameManager.gamePower;
        ulong needPow = E_sports_Array[x].winPower;

        int winProbability, secondPlaceProbability, loseProbability;

        if (playerPower >= needPow)
        {
            // 플레이어 게임력이 기준 게임력보다 높을 경우
            // 게임력 차이에 비례하여 우승 확률을 계산
            ulong difference = playerPower - needPow;
            winProbability = (int)Math.Min(100, 85 + difference / 800); // 게임력 차이에 따른 확률 조정 필요
            secondPlaceProbability = 100 - winProbability;
            loseProbability = 100 - secondPlaceProbability;
        }
        else
        {
            // 플레이어 게임력이 기준 게임력보다 낮을 경우
            int difference = (int)(needPow - playerPower);
            winProbability = Math.Max(0, (int)Math.Log(difference)*2);
            secondPlaceProbability = Math.Max(0, (int)Math.Log(difference)*4);
            loseProbability = 100 - winProbability - secondPlaceProbability;
        }

        // 이제 winProbability, secondPlaceProbability, loseProbability를 사용하여 게임 결과를 결정할 수 있습니다.
        // 예를 들어, 랜덤한 수를 생성하고 이 수가 각 확률 범위에 속하는지 확인하여 게임 결과를 결정할 수 있습니다.

        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 101); // 1부터 100까지의 랜덤한 수를 생성합니다.

        // 이 랜덤한 수가 각 확률 범위에 속하는지 확인하여 게임 결과를 결정합니다.
        if (randomNumber <= winProbability)
        {
            // 우승
            Debug.Log("Win!");
        }
        else if (randomNumber <= winProbability + secondPlaceProbability)
        {
            // 준우승
            Debug.Log("Second place!");
        }
        else
        {
            // 패배
            Debug.Log("Lose!");
        }
        GameManager.gamePower -= needPow;
    }
}