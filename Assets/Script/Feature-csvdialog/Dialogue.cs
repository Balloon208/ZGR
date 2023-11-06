using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dialogue : EventManager
{
    [SerializeField] private TextAsset csvFile = null;
    public static Dictionary<string, TalkData[]> DialogueDictionary = new Dictionary<string, TalkData[]>();

    public class TalkData
    {
        public string name; // ��� ġ�� ĳ���� �̸�
        public string[] contexts; // ��� ����
        public string[] seteventname;
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
                List<string> seteventList = new List<string>();

                TalkData talkData = new TalkData();
                talkData.name = rowvalues[1]; // ĳ���� �̸� ��

                do // talkData �ϳ��� ����� �ݺ���
                {
                    contextList.Add(rowvalues[2].ToString());
                    seteventList.Add(rowvalues[3].ToString());
                    if (++i < rows.Length)
                        rowvalues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowvalues[1] == "" && rowvalues[0] != "end"); // ��, �ѻ���� ��縦 ��~�� �־��ִ� ���̴�!

                talkData.contexts = contextList.ToArray();
                talkData.seteventname = seteventList.ToArray();

                talkdatalist.Add(talkData);
            }

            Debug.Log(talkdatalist.Count);
            DialogueDictionary.Add(eventname, talkdatalist.ToArray());

        }
    }

    protected IEnumerator Fullshow(string eventname, bool recursive) // override or not
    {
        if (DialogueDictionary.ContainsKey(eventname))
        {
            float temp = playerMove.MoveSpeed;
            if (!recursive)
            {
                playerMove.MoveSpeed = 0;
                ChatUI.SetActive(true);
                scriptlock = true;
            }
            

            for (int i = 0; i < DialogueDictionary[eventname].Length; i++)
            {
                string name = DialogueDictionary[eventname][i].name;

                if (name == "Select")
                {
                    Debug.Log("Select");
                    List<string> selectlist = new List<string>();
                    List<string> seteventList = new List<string>();
                    for (int j = 0; j < DialogueDictionary[eventname][i].contexts.Length; j++)
                    {
                        selectlist.Add(DialogueDictionary[eventname][i].contexts[j]);
                        seteventList.Add(DialogueDictionary[eventname][i].seteventname[j]);
                    }

                    yield return ts.Selecting(2, selectlist[0], selectlist[1]);
                    k = ts.cursor;
                    eventName = seteventList[k - 1].Trim();
                    yield return StartCoroutine(Fullshow(eventName, true));
                }
                else if(name == "Eventset")
                {
                    eventName = DialogueDictionary[eventname][i].seteventname[0].Trim();
                    yield return null;
                }
                else if(name == "Give")
                {
                    string[] item = DialogueDictionary[eventname][i].contexts[0].Split("^");
                    InventoryManager.Instance.Additem(int.Parse(item[0]), int.Parse(item[1]));
                    yield return null;
                }
                else
                {
                    string nextname = "";
                    if(i < DialogueDictionary[eventname].Length-1) nextname = DialogueDictionary[eventname][i + 1].name;
                    Debug.Log(nextname);
                    for (int j = 0; j < DialogueDictionary[eventname][i].contexts.Length; j++)
                    {
                        string text = DialogueDictionary[eventname][i].contexts[j];

                        bool noskip = true;
                        if (nextname == "Select")
                        {
                            noskip = false;
                        }
                        yield return ts.ShowText(name, text, noskip);
                    }
                }
                
                
            }

            if(!recursive)
            {
                scriptlock = false;
                ChatUI.SetActive(false);
                playerMove.MoveSpeed = temp;
            }
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
            StartCoroutine(Fullshow(eventName, false));
        }
    }
}

