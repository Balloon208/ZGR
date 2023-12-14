using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestType
{
    Talk,
    Grind,
    Slain
}

public enum QuestProcess
{
    Unstartable,
    Startable,
    Processing,
    Completed
}

public class Quest {
    public int questid;
    public string tag;
    public string questname;
    public string startNPC;
    public string endNPC;
    public QuestType questtype;
    public int targetid;
    public int amount;
    public int rewardEXP;
    public int rewardGold;
    public List<Tuple<int, int>> rewarditem = new List<Tuple<int, int>>();
    public int requirequestid;
    public int nextquestid;
    public QuestProcess questprocess;

    public Quest(string[] args)
    {
        if (!int.TryParse(args[0], out questid))
        {
            questid = 0;
        }
        tag = args[1];
        questname = args[2];
        startNPC = args[3];
        endNPC = args[4];
        questtype = (QuestType)Enum.Parse(typeof(QuestType), args[5]);
        if (!int.TryParse(args[6], out targetid))
        {
            targetid = 0;
        }
        if (!int.TryParse(args[7], out amount))
        {
            amount = 0;
        }
        if (!int.TryParse(args[8], out rewardEXP))
        {
            rewardEXP = 0;
        }
        if (!int.TryParse(args[9], out rewardGold))
        {
            rewardGold = 0;
        }
        if (!string.IsNullOrEmpty(args[10]))
        {
            // args[10] 에서 a^b 구조를 가지므로, FormatException은 일어나지 않는다.
            rewarditem.AddRange(args[10].Split('|')
                .Select(pair => Tuple.Create(int.Parse(pair.Split('^')[0]), int.Parse(pair.Split('^')[1])))
            );
        }
        requirequestid = int.Parse(args[11]);
        nextquestid = int.Parse(args[12]);

        // save load 때 수정 필요
        if (requirequestid == 0)
        {
            questprocess = QuestProcess.Startable;
        }
        else
        {
            questprocess = QuestProcess.Unstartable;
        }
    }


    protected void QuestCheck()
    {
        return;
    }
}
