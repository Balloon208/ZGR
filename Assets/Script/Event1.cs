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
