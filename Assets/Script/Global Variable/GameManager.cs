using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Item[] itemDB;
    public Item[] useritems;
    public int itemcount = 0;
    private GameObject Inventory;
    private bool Inventoryopen = false;
    private void Awake()
    {
        Inventory = GameObject.Find("Inventory");
        Inventory.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Inventoryopen = !Inventoryopen;
            Inventory.SetActive(Inventoryopen);
        }
    }
}
