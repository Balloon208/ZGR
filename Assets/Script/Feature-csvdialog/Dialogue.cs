using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dialogue : EventManager
{
    [SerializeField] private TextAsset csvFile = null;
    public static Dictionary<string, TalkData[]> DialogueDictionary = new Dictionary<string, TalkData[]>();

    public struct TalkData
    {
        public string name; // ��� ġ�� ĳ���� �̸�
        public string[] contexts; // ��� ����
    }

    // ��ȭ �̺�Ʈ �̸�
    [SerializeField] string eventName;

    public void SetDialogue()
    {
        // �� �Ʒ� �� �� ����
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });

        for(int i = 1; i < rows.Length; i++) // 0�� ���� tag �̹Ƿ�, �ش� �κ��� �˻����� ����
        {
            string[] rowvalues = rows[i].Split(new char[] { ',' }); // split ������ ,
            if (rowvalues[0].Trim() == "" || rowvalues[0].Trim() == "end") continue;

            // �̺�Ʈ �̸��� ������, end ������ ���� �Է��� �־��ݴϴ�.
            List<TalkData> talkdatalist = new List<TalkData>();
            string eventname = rowvalues[0];

            while (rowvalues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();

                TalkData talkData;
                talkData.name = rowvalues[1]; // ĳ���� �̸� ��

                do // talkData �ϳ��� ����� �ݺ���
                {
                    contextList.Add(rowvalues[2].ToString());
                    if (++i < rows.Length)
                        rowvalues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowvalues[1] == "" && rowvalues[0] != "end"); // ��, �ѻ���� ��縦 ��~�� �־��ִ� ���̴�!

                talkData.contexts = contextList.ToArray();
                talkdatalist.Add(talkData);
            }

            Debug.Log(talkdatalist.Count);
            DialogueDictionary.Add(eventname, talkdatalist.ToArray());

        }
    }

    protected IEnumerator Fullshow(string eventname) // override or not
    {
        if (DialogueDictionary.ContainsKey(eventname))
        {
            float temp = playerMove.MoveSpeed;
            playerMove.MoveSpeed = 0;
            ChatUI.SetActive(true);
            scriptlock = true;

            for (int i = 0; i < DialogueDictionary[eventname].Length; i++)
            {
                string name = DialogueDictionary[eventname][i].name;

                for(int j = 0; j < DialogueDictionary[eventname][i].contexts.Length; j++)
                {
                    string text = DialogueDictionary[eventname][i].contexts[j];
                    yield return ts.ShowText(name, text, true);
                }
                
            }

            scriptlock = false;
            ChatUI.SetActive(false);
            playerMove.MoveSpeed = temp;
        }
        else
        {
            // ��� ����ϰ� null ��ȯ
            Debug.LogWarning("ã�� �� ���� �̺�Ʈ �̸� : " + eventname);
            yield return null;
        }
        
    }
    private void Start()
    {
        SetDialogue();
    }

    public void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Fullshow(eventName));
        }
    }
}

