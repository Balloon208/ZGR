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
        public string name; // 대사 치는 캐릭터 이름
        public string[] contexts; // 대사 내용
        public string[] seteventname;
    }

    // 대화 이벤트 이름
    [SerializeField] string eventName;

    public void SetDialogue()
    {
        // 맨 아래 한 줄 빼기
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        for(int i = 1; i < rows.Length; i++) // 0번 줄은 tag 이므로, 해당 부분은 검사하지 않음
        {
            string[] rowvalues = rows[i].Split(new char[] { ',' }); // split 조건은 ,
            if (rowvalues[0].Trim() == "" || rowvalues[0].Trim() == "end") continue;

            // 이벤트 이름을 만나면, end 만날때 까지 입력을 넣어줍니다.
            List<TalkData> talkdatalist = new List<TalkData>();
            string eventname = rowvalues[0];

            while (rowvalues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                List<string> seteventList = new List<string>();

                TalkData talkData = new TalkData();
                talkData.name = rowvalues[1]; // 캐릭터 이름 열

                do // talkData 하나를 만드는 반복문
                {
                    contextList.Add(rowvalues[2].ToString());
                    seteventList.Add(rowvalues[3].ToString());
                    if (++i < rows.Length)
                        rowvalues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowvalues[1] == "" && rowvalues[0] != "end"); // 즉, 한사람의 대사를 전~부 넣어주는 것이다!

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
            // 경고 출력하고 null 반환
            Debug.LogWarning("찾을 수 없는 이벤트 이름 : " + eventname);
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

