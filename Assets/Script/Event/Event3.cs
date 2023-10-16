using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : EventManager
{
    protected override IEnumerator Fullshow() // override or not
    {
        float temp = playerMove.MoveSpeed;
        playerMove.MoveSpeed = 0;
        ChatUI.SetActive(true);
        scriptlock = true;

        if (GameManager.instance.QuestTrigger[0] == 0 || GameManager.instance.QuestTrigger[0] == 2)
        {
            yield return ts.ShowText("Jarry", "I'm Jarry. and my friend's name is Jerry!", true);
        }
        else if (GameManager.instance.QuestTrigger[0] == 1)
        {
            yield return ts.ShowText("Jarry", "Oh, Will you receive a gift?", false);
            yield return ts.Selecting(2, "YES!", "NO!");
            k = ts.cursor;
            if (k == 1)
            {
                yield return ts.ShowText("Jerry", "Here you are.", true);
                GameManager.instance.QuestTrigger[0] = 2;
            }
            if (k == 2)
            {
                yield return ts.ShowText("Jerry", "Come again if you want to give a gift", true);
            }
        }

        scriptlock = false;
        ChatUI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }
}
