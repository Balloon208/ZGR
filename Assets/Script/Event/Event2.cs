using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : EventManager
{
    protected override IEnumerator Fullshow() // override or not
    {
        float temp = playerMove.MoveSpeed;
        playerMove.MoveSpeed = 0;
        UI.SetActive(true);
        scriptlock = true;

        if (GlobalValues.QuestTrigger[0] == 0)
        {
            yield return ts.ShowText("Jerry", "Is Jerry smart?????", false);
            yield return ts.Selecting(2, "YES!!!!!!", "NO!!!!!!");
            k = ts.cursor;
            if (k == 1)
            {
                yield return ts.ShowText("Jerry", "Then, Go to Jarry!", true);
                yield return ts.ShowText("Jerry", "He will give you a gift!", true);
                GlobalValues.QuestTrigger[0] = 1;
            }
            if (k == 2)
            {
                yield return ts.ShowText("Jerry", "I will kill you in Hypixel Skyblock...", true);
            }
        }
        else if (GlobalValues.QuestTrigger[0] == 1)
        {
            yield return ts.ShowText("Jerry", "Go to Jarry and receive my gift!", true);
        }
        else if (GlobalValues.QuestTrigger[0] == 2)
        {
            yield return ts.ShowText("Jerry", "Good Luck!!!! XD", true);
        }

        scriptlock = false;
        UI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }
}
