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
        ChatUI.SetActive(true);
        scriptlock = true;

        yield return ts.ShowText("�����", "���� �ű� ��! �̿��� ������ ���� �����ұ� �������ε�, ��� ������?", false);
        yield return ts.Selecting(2, "��û����!", "�����ؿ�");
        k = ts.cursor;
        if (k == 1)
        {
            yield return ts.ShowText("�����", "�̿��ø� �׷��� ���Ѵٸ��, �ļ����� ���ֵ��� ����.", true);
        }
        if (k == 2)
        {
            yield return ts.ShowText("�����", "�ƹ��� �Ⱦ ������ �������...", false);
            yield return ts.Selecting(2, "�̾��ؿ�", "���� ������");
            k = ts.cursor;
            if (k == 1)
            {
                yield return ts.ShowText("�����", "������, �������� �̷� ������ ��������.", true);
            }
            if (k == 2)
            {
                yield return ts.ShowText("-", "�״� �Ǹ������� ǥ���� �ϰ� �������ȴ�.", true);
            }
        }
        scriptlock = false;
        ChatUI.SetActive(false);
        playerMove.MoveSpeed = temp;
    }
}
