using System.Collections.Generic;

public class UserData : ISaveable
{
    public UserData()
    {
        Energy = 100;
        Rewards = new RewardEntity();
        QuestsData = new List<QuestEntity>();
        CompletedMinigamesList = new List<int>();
    }

    public void CompleteQuest(QuestEntity questEntity)
    {
        Rewards.Level += questEntity.Rewards.Level;
        Rewards.Money += questEntity.Rewards.Money;
        Energy -= questEntity.EnergyCosts;
        CompletedMinigamesList.Add(questEntity.QuestID);
    }

    public int Energy;
    public RewardEntity Rewards;
    public List<int> CompletedMinigamesList;
    public List<QuestEntity> QuestsData;
}
