using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event1 : EventManager
{
    protected override IEnumerator Fullshow() // override or not
    {
        float temp = playerMove.MoveSpeed;
        playerMove.MoveSpeed = 0;
        UI.SetActive(true);
        scriptlock = true;

        StartCoroutine(ts.ShowText("김민준", "어이 거기 너! 미연시 게임을 새로 개발할까 생각중인데, 어떻게 생각해?", false));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        StartCoroutine(ts.Selecting(2, "엄청나요!", "끔찍해요"));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        k = ts.cursor;
        if (k == 1)
        {
            StartCoroutine(ts.ShowText("김민준", "미연시를 그렇게 원한다면야, 후속작을 내주도록 하지.", true));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
        }
        if (k == 2)
        {
            StartCoroutine(ts.ShowText("김민준", "아무리 싫어도 끔찍할 정도라니...", false));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            StartCoroutine(ts.Selecting(2, "미안해요", "저리 가세요"));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            k = ts.cursor;
            if (k == 1)
            {
                StartCoroutine(ts.ShowText("김민준", "괜찮아, 다음부턴 이런 말들은 조심해줘.", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
            if (k == 2)
            {
                StartCoroutine(ts.ShowText("-", "그는 실망스러운 표정을 하곤 떠나버렸다.", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
        }
        scriptlock = false;
        UI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }

    private void Update() // add
    {
        Debug.DrawRay(gameObject.transform.position, Vector2.up, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.down, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.left, Color.red);
        Debug.DrawRay(gameObject.transform.position, Vector2.right, Color.red);
        if (Input.GetKeyDown(KeyCode.C) && scriptlock == false)
        {
            CheckHit();
        }
    }
}
