using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private TextAsset csvFile = null;
    public Dictionary<int, Quest> QuestDictionary = new Dictionary<int, Quest>();

    void SetQuest()
    {
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });

        for (int i = 1; i < rows.Length; i++) // 0번 줄은 tag 이므로, 해당 부분은 검사하지 않음
        {
            string[] rowvalues = rows[i].Split(new char[] { ',' }); // split 조건은 ,
            if (rowvalues[0].Trim() == "" || rowvalues[0].Trim() == "end") continue;

            // 이벤트 이름을 만나면, end 만날때 까지 입력을 넣어줍니다.
            int questid = int.Parse(rowvalues[0]);
            string startnpc = rowvalues[3];

            QuestDictionary[questid] = new Quest(rowvalues);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}