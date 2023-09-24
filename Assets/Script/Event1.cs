using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event1 : MonoBehaviour
{
    public int k = 1;
    TextScript ts;

    public void 대화()
    {
        Debug.Log("대화");
    }

    private void Awake()
    {
        ts = GameObject.Find("GameManager").GetComponent<TextScript>();
    }
    IEnumerator Fullshow()
    {
        대화();
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
        
    }

    public void StartScript()
    {
        StartCoroutine(Fullshow());
    }
}
