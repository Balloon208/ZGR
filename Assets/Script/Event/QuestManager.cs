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
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });

        for (int i = 1; i < rows.Length; i++) // 0�� ���� tag �̹Ƿ�, �ش� �κ��� �˻����� ����
        {
            string[] rowvalues = rows[i].Split(new char[] { ',' }); // split ������ ,
            if (rowvalues[0].Trim() == "" || rowvalues[0].Trim() == "end") continue;

            // �̺�Ʈ �̸��� ������, end ������ ���� �Է��� �־��ݴϴ�.
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