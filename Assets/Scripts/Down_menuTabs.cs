using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_menuTabs : MonoBehaviour
{
    public int myScroll;
    public GameObject[] anotherScrollviews;
    public GameObject E_Sports;
    
    public void UI_change(int x)
    {
        for(int i = 0; i < anotherScrollviews.Length; i++)
        {
            anotherScrollviews[i].SetActive(false);
        }
        switch (x)
        {
            case 0:
                anotherScrollviews[0].SetActive(true);
                break;
            case 1:
                anotherScrollviews[1].SetActive(true);
                break;
            case 2:
                anotherScrollviews[2].SetActive(true);
                break;
            case 3:
                anotherScrollviews[3].SetActive(true);
                break;
            case 4:
                anotherScrollviews[4].SetActive(true);
                break;
        }
    }

    public void E_Sports_UIon()
    {
        E_Sports.SetActive(true);
    }

    public void E_Sports_UIoff()
    {
        E_Sports.SetActive(false);
    }
}