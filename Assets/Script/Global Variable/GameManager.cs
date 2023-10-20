using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Item[] items;
    public int itemcount = 0;
    public int[] QuestTrigger;
    private GameObject Inventory;
    private bool Inventoryopen = false;
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
