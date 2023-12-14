using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kimminjun : NPC
{
    private int[] questlist = { 1, 2, 3 };
    protected override void setEventName()
    {
        eventName = "Normal";
        if (QuestManager.Instance.QuestDictionary[1].questprocess == QuestProcess.Startable)
        {
            eventName = "Quest1Main";
        }
        if (QuestManager.Instance.QuestDictionary[1].questprocess == QuestProcess.Processing)
        {
            eventName = "Quest1Processing";
        }
        else if (QuestManager.Instance.QuestDictionary[1].questprocess == QuestProcess.Completed)
        {
            eventName = "Quest1Completed";

            int nextid = QuestManager.Instance.QuestDictionary[1].nextquestid;
            QuestManager.Instance.QuestDictionary[1].questprocess = QuestProcess.Unstartable;
            QuestManager.Instance.QuestDictionary[nextid].questprocess = QuestProcess.Startable;
        }
    }
}
