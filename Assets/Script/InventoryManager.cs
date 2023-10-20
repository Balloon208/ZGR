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

    public void Additem(Item item)
    {
        int count = GameManager.instance.itemcount;
        for (int i = 0; i < GameManager.instance.itemcount; i++)
        {
            if (GameManager.instance.items[i].id == item.id)
            {
                GameManager.instance.items[i].amount++;
                Debug.Log("Find");
                return;
            }
        }
        GameManager.instance.items[count] = item;
        GameManager.instance.items[count].amount = 1;
        GameManager.instance.itemcount++;
        Debug.Log("Not Find");
        
    }
}
