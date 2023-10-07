using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : EventManager
{
    protected override IEnumerator Fullshow() // override or not
    {
        float temp = playerMove.MoveSpeed;
        playerMove.MoveSpeed = 0;
        UI.SetActive(true);
        scriptlock = true;

        if (GlobalValues.QuestTrigger[0] == 0 || GlobalValues.QuestTrigger[0] == 2)
        {
            StartCoroutine(ts.ShowText("Jarry", "I'm Jarry. and my friend's name is Jerry!", true));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
        }
        else if (GlobalValues.QuestTrigger[0] == 1)
        {
            StartCoroutine(ts.ShowText("Jarry", "Oh, Will you receive a gift?", false));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            StartCoroutine(ts.Selecting(2, "YES!", "NO!"));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            k = ts.cursor;
            if (k == 1)
            {
                StartCoroutine(ts.ShowText("Jerry", "Here you are.", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
                GlobalValues.QuestTrigger[0] = 2;
            }
            if (k == 2)
            {
                StartCoroutine(ts.ShowText("Jerry", "Come again if you want to give a gift", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
        }

        scriptlock = false;
        UI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }
}
