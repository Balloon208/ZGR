using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[,] slots;

    public void SetInventory()
    {
        for(int i = 0; i < GameManager.instance.useritems.Length; i++)
        {
            GameObject slot = GameObject.Find(string.Format("Slot({0},{1})", i / 8, i % 8));
            Image slotimage = slot.GetComponent<Image>();
            Text value = slot.transform.Find("Value").GetComponent<Text>();

            if (i < GameManager.instance.itemcount)
            {
                slotimage.sprite = GameManager.instance.useritems[i].itemImage;
                value.text = GameManager.instance.useritems[i].amount.ToString();
                slotimage.color = new Color(255, 255, 255, 1f);
            }
            else
            {
                slotimage.sprite = null;
                value.text = "";
                slotimage.color = new Color(255, 255, 255, 0f);
            }
        }
    }
}
