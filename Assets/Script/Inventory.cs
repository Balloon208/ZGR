using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[,] slots;

    public void SetInventory()
    {
        for(int i = 0; i < GameManager.Instance.useritems.Length; i++)
        {
            GameObject slot = GameObject.Find(string.Format("Slot({0},{1})", i / 8, i % 8));
            Image slotimage = slot.GetComponent<Image>();
            Text value = slot.transform.Find("Value").GetComponent<Text>();

            if (i < GameManager.Instance.itemcount)
            {
                slotimage.sprite = GameManager.Instance.useritems[i].itemImage;
                value.text = GameManager.Instance.useritems[i].amount.ToString();
                slotimage.color = new Color(255, 255, 255, 1f);
            }
            else
            {
                slotimage.sprite = null;
                value.text = string.Empty;
                slotimage.color = new Color(255, 255, 255, 0f);
            }
        }
    }
}
