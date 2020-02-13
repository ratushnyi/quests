using System;
using System.Collections.Generic;

public interface IMainController
{
    event Action<List<QuestEntity>> OnQuestLinesNeedToUpdate;
}
