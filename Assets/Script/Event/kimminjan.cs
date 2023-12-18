using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kimminjan : NPC
{
    private int[] questlist = { 1, 2, 3 };
    protected override void setEventName()
    {
        eventName = "Normal";
        if (QuestManager.Instance.QuestDictionary[1].questprocess == QuestProcess.Processing)
        {
            if(InventoryManager.Instance.Finditem(1) >= 5)
            {
                eventName = "Quest1ProcessingSuccess";
            }
            else
            {
                eventName = "Quest1ProcessingFail";
            }
        }
        else if (QuestManager.Instance.QuestDictionary[2].questprocess == QuestProcess.Startable)
        {
            Debug.Log("test success! do commit.");
        }
    }
}
