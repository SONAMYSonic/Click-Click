using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public int Quantity;
    public string Effect;

    public Item() { }

    public Item(string name, string effect, int quantity)
    {
        Name = name;
        Effect = effect;
        Quantity = quantity;
    }
}

public class Game_Item : MonoBehaviour
{
    public GameManager GameManager;                         //게임매니저 참조

    // 아이템 배열
    public Item[] items;

    // 버튼에 표시할 아이템 텍스트 배열
    public TMPro.TMP_Text[] itemTexts;

    private bool isYuDongDoubleActive;
    
    public void ActivateYuDongDouble()
    {
        if (isYuDongDoubleActive || items[0].Quantity <= 0)
        {
            Debug.Log("Already activated or insufficient quantity!");
            return;
        }
        StartCoroutine(YuDongDoubleCoroutine());
        items[0].Quantity--;
        UpdateItemTexts();
    }

    private IEnumerator YuDongDoubleCoroutine()
    {
        isYuDongDoubleActive = true;
        yield return new WaitForSeconds(30); // 30초 동안 대기
        isYuDongDoubleActive = false;
    }

    public ulong GetGamePowerMultiplier()
    {
        return isYuDongDoubleActive ? (ulong)2 : (ulong)1;
    }

    // 아이템 버튼 텍스트 업데이트
    public void UpdateItemTexts()
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemTexts[i].text = items[i].Name + "\n" + items[i].Effect + "\n" + "보유량: "+ items[i].Quantity;
        }
    }
}
