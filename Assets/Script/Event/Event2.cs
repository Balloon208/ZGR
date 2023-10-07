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
            StartCoroutine(ts.ShowText("Jerry", "Is Jerry smart?????", false));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            StartCoroutine(ts.Selecting(2, "YES!!!!!!", "NO!!!!!!"));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            k = ts.cursor;
            if (k == 1)
            {
                StartCoroutine(ts.ShowText("Jerry", "Then, Go to Jarry!", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
                StartCoroutine(ts.ShowText("Jerry", "He will give you a gift!", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
                GlobalValues.QuestTrigger[0] = 1;
            }
            if (k == 2)
            {
                StartCoroutine(ts.ShowText("Jerry", "I will kill you in Hypixel Skyblock...", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
        }
        else if (GlobalValues.QuestTrigger[0] == 1)
        {
            StartCoroutine(ts.ShowText("Jerry", "Go to Jarry and receive my gift!", true));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
        }
        else if (GlobalValues.QuestTrigger[0] == 2)
        {
            StartCoroutine(ts.ShowText("Jerry", "Good Luck!!!! XD", true));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
        }


        scriptlock = false;
        UI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }
}
