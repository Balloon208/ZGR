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
        int count = GameManager.Instance.itemcount;
        for (int i = 0; i < GameManager.Instance.itemcount; i++)
        {
            if (GameManager.Instance.useritems[i].id == itemid)
            {
                GameManager.Instance.useritems[i].amount+=amount;
                Debug.Log("Find");
                return;
            }
        }
        GameManager.Instance.useritems[count] = GameManager.Instance.itemDB[itemid];
        GameManager.Instance.useritems[count].amount = amount;
        GameManager.Instance.itemcount++;
        Debug.Log("Not Find");
        
    }
}
