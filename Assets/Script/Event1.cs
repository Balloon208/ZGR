using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event1 : MonoBehaviour
{
    public int k = 1;
    TextScript ts;

    private void Awake()
    {
        ts = GameObject.Find("GameManager").GetComponent<TextScript>();
    }
    IEnumerator Fullshow()
    {
        StartCoroutine(ts.ShowText("±è¹ÎÁØ", "asdfiwpojpofejwaopfjwopfjwopfjewopf1234"));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        StartCoroutine(ts.Selecting(4, "asdf", "asdf", "asdf", "asdf"));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        StartCoroutine(ts.ShowText("±è¹ÎÁØ", "asdfiwpojpofejwaopfjwopfjwopfjewopf2345"));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
    }

    public void StartScript()
    {
        StartCoroutine(Fullshow());
    }
}
