using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestEntityScriptable", menuName = "ScriptableObjects/QuestEntity")]
public class QuestEntity : ScriptableObject, IQuest
{
    public string Name;
    public bool IsComplete;
    public int EnergyCosts;
    public int QuestID;
    public List<ConditionEntity> Conditions;
    public RewardEntity Rewards;

    public (bool isPassed, string info) AttemptToPass(UserData usedData)
    {
        var causes = "Congratulations, you have successfully completed the quest";
        var result = true;

        foreach (var item in Conditions)
        {
            switch (item.Condition)
            {
                case ConditionTypes.MinigameComplete:
                    if (!usedData.CompletedMinigamesList.Contains(item.QuestID))
                    {
                        AddCause(ref result, ref causes, "you are not completed all quest");
                    }
                    break;
                default:
                    if (usedData.Rewards.Level < item.MinLevel)
                    {
                        AddCause(ref result, ref causes, "yours level is too low");
                    }
                    break;
            }
        }

        if (EnergyCosts > usedData.Energy)
        {
            AddCause(ref result, ref causes, $"yours level of energy is too low, you need min {EnergyCosts} points of energy");
        }

        causes += ".";
        return (result, causes);
    }

    public void AddCause(ref bool result, ref string causes, string currentCause)
    {
        if (result)
        {
            causes = Consts.UnfulfilledConditions;
        }

        if (causes != Consts.UnfulfilledConditions)
        {
            causes += " and ";
        }

        causes += currentCause;

        result = false;
    }
}
