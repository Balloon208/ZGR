using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public void test()
    {
        Additem(1, 1);
    }
    public void Additem(int itemid, int amount)
    {
        int itemcount = GameManager.Instance.itemcount;
        for (int i = 0; i < itemcount; i++)
        {
            // 아이템이 존재한다면 개수만 증가
            if (GameManager.Instance.useritems[i].id == itemid)
            {
                GameManager.Instance.useritems[i].amount+=amount;
                Debug.Log("Find");
                return;
            }
        }
        GameManager.Instance.useritems[itemcount] = GameManager.Instance.itemDB[itemid];
        GameManager.Instance.useritems[itemcount].amount = amount;
        GameManager.Instance.itemcount++;
        Debug.Log("Not Find");
    }

    public int Finditem(int itemid)
    {
        for (int i = 0; i < GameManager.Instance.itemcount; i++)
        {
            // 아이템이 존재한다면 개수만 증가
            if (GameManager.Instance.useritems[i].id == itemid)
            {
                return GameManager.Instance.useritems[i].amount;
            }
        }
        return 0;
    }
}
