using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event1 : MonoBehaviour
{
    public int k = 1;
    TextScript ts;

    public void ��ȭ()
    {
        Debug.Log("��ȭ");
    }

    private void Awake()
    {
        ts = GameObject.Find("GameManager").GetComponent<TextScript>();
    }
    IEnumerator Fullshow()
    {
        ��ȭ();
        StartCoroutine(ts.ShowText("�����", "���� �ű� ��! �̿��� ������ ���� �����ұ� �������ε�, ��� ������?", false));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        StartCoroutine(ts.Selecting(2, "��û����!", "�����ؿ�"));
        yield return new WaitUntil(() => ts.coroutine_lock == false);
        k = ts.cursor;
        if (k == 1)
        {
            StartCoroutine(ts.ShowText("�����", "�̿��ø� �׷��� ���Ѵٸ��, �ļ����� ���ֵ��� ����.", true));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
        }
        if (k == 2)
        {
            StartCoroutine(ts.ShowText("�����", "�ƹ��� �Ⱦ ������ �������...", false));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            StartCoroutine(ts.Selecting(2, "�̾��ؿ�", "���� ������"));
            yield return new WaitUntil(() => ts.coroutine_lock == false);
            k = ts.cursor;
            if (k == 1)
            {
                StartCoroutine(ts.ShowText("�����", "������, �������� �̷� ������ ��������.", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
            if (k == 2)
            {
                StartCoroutine(ts.ShowText("-", "�״� �Ǹ������� ǥ���� �ϰ� �������ȴ�.", true));
                yield return new WaitUntil(() => ts.coroutine_lock == false);
            }
        }
        
    }

    public void StartScript()
    {
        StartCoroutine(Fullshow());
    }
}
