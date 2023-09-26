using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event1 : MonoBehaviour
{
    PlayerMove playerMove;
    GameObject UI;
    private bool scriptlock = false;
    private int k = 1;
    TextScript ts;
    RaycastHit2D lookhit;

    void CheckHit()
    {
        lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero, 0f);

        if (playerMove.look == Vector2.down) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.up) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.right) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1f, LayerMask.GetMask("PlayerEntity"));
        if ((lookhit.collider == null || lookhit.collider.tag != "Player") && playerMove.look == Vector2.left) lookhit = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1f, LayerMask.GetMask("PlayerEntity"));
        if (lookhit.collider != null && lookhit.collider.tag == "Player")
        {
            Debug.Log("asdfghjuytrdv");
            StartCoroutine(Fullshow());
        }
    }

    private void Awake()
    {
        ts = GameObject.Find("GameManager").GetComponent<TextScript>();
        UI = GameObject.Find("Canvas").transform.Find("UI").gameObject;
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }
    IEnumerator Fullshow()
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

    private void Update()
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
