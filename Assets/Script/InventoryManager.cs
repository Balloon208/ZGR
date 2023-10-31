using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public static InventoryManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            else
            {
                return instance;
            }
        }
    }

    public void test()
    {
        Additem(1, 1);
    }
    public void Additem(int itemid, int amount)
    {
        int count = GameManager.instance.itemcount;
        for (int i = 0; i < GameManager.instance.itemcount; i++)
        {
            if (GameManager.instance.useritems[i].id == itemid)
            {
                GameManager.instance.useritems[i].amount+=amount;
                Debug.Log("Find");
                return;
            }
        }
        GameManager.instance.useritems[count] = GameManager.instance.itemDB[itemid];
        GameManager.instance.useritems[count].amount = amount;
        GameManager.instance.itemcount++;
        Debug.Log("Not Find");
        
    }
}
