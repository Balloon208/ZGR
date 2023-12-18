using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class NPC : EventManager
{
    [SerializeField] private TextAsset csvFile = null;
    public static Dictionary<string, Dictionary<string, TalkData[]>> DialogueDictionary = new Dictionary<string, Dictionary<string, TalkData[]>>();

    public class TalkData
    {
        public string name; // ��� ġ�� ĳ���� �̸�
        public string[] contexts; // ��� ����
        public string[] seteventname;
    }

    // ��ȭ �̺�Ʈ �̸�
    public string eventName;

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
            
            if(!DialogueDictionary.ContainsKey(gameObject.name))
            {
                DialogueDictionary[gameObject.name] = new Dictionary<string, TalkData[]>();
            }
            DialogueDictionary[gameObject.name][eventname] = talkdatalist.ToArray();
        }
    }

    protected virtual void setEventName()
    {
        return;
    }

    protected override IEnumerator Fullshow(bool recursive) // override or not
    {
        if (!recursive)
        {
            setEventName();
        }

        if (DialogueDictionary[gameObject.name].ContainsKey(eventName))
        {
            float temp = playerMove.MoveSpeed;
            if (!recursive)
            {
                playerMove.MoveSpeed = 0;
                ChatUI.SetActive(true);
                scriptlock = true;
            }

            for (int i = 0; i < DialogueDictionary[gameObject.name][eventName].Length; i++)
            {
                string name = DialogueDictionary[gameObject.name][eventName][i].name;

                if (name == "Select")
                {
                    Debug.Log("Select");
                    List<string> selectlist = new List<string>();
                    List<string> seteventList = new List<string>();
                    for (int j = 0; j < DialogueDictionary[gameObject.name][eventName][i].contexts.Length; j++)
                    {
                        selectlist.Add(DialogueDictionary[gameObject.name][eventName][i].contexts[j]);
                        seteventList.Add(DialogueDictionary[gameObject.name][eventName][i].seteventname[j]);
                    }

                    yield return ts.Selecting(2, selectlist[0], selectlist[1]);
                    k = ts.cursor;
                    eventName = seteventList[k - 1].Trim();
                    yield return StartCoroutine(Fullshow(true));
                }
                else if(name == "SetQuest")
                {
                    string[] item = DialogueDictionary[gameObject.name][eventName][i].contexts[0].Split("^");
                    QuestManager.Instance.QuestDictionary[int.Parse(item[0])].questprocess = (QuestProcess)Enum.Parse(typeof(QuestProcess),item[1]);
                    yield return null;
                }
                else if(name == "Give")
                {
                    string[] item = DialogueDictionary[gameObject.name][eventName][i].contexts[0].Split("^");
                    InventoryManager.Instance.Additem(int.Parse(item[0]), int.Parse(item[1]));
                    yield return null;
                }
                else
                {
                    string nextname = "";
                    if(i < DialogueDictionary[gameObject.name][eventName].Length-1) nextname = DialogueDictionary[gameObject.name][eventName][i + 1].name;
                    Debug.Log(nextname);
                    for (int j = 0; j < DialogueDictionary[gameObject.name][eventName][i].contexts.Length; j++)
                    {
                        string text = DialogueDictionary[gameObject.name][eventName][i].contexts[j];

                        bool noskip = true; // skip
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
            Debug.LogWarning("ã�� �� ���� �̺�Ʈ �̸� : " + eventName);
            yield return null;
        }
        
    }
    private void Start()
    {
        SetDialogue();
    }
}

