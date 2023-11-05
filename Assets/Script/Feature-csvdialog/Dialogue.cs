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
        public string name; // 대사 치는 캐릭터 이름
        public string[] contexts; // 대사 내용
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

                TalkData talkData;
                talkData.name = rowvalues[1]; // 캐릭터 이름 열

                do // talkData 하나를 만드는 반복문
                {
                    contextList.Add(rowvalues[2].ToString());
                    if (++i < rows.Length)
                        rowvalues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowvalues[1] == "" && rowvalues[0] != "end"); // 즉, 한사람의 대사를 전~부 넣어주는 것이다!

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
            StartCoroutine(Fullshow(eventName));
        }
    }
}

