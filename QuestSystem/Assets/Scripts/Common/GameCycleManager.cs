using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class GameCycleManager
{
    public event Action<List<QuestEntity>> OnQuestLinesHaveDataToUpdate;

    private IMainController _controller;
    private List<QuestEntity> _questLine;
    private List<QuestEntity> _newData;
    public GameCycleManager(IMainController controller)
    {
        _controller = controller;
        _questLine = new List<QuestEntity>();
        _newData = new List<QuestEntity>();

        controller.OnQuestLinesNeedToUpdate += SearchFreeQuests;
    }

    private void SearchFreeQuests(List<QuestEntity> questLine)
    {
        var files = Directory.GetFiles(Consts.QuestsEntitiesPath).ToList();
        foreach (var item in files)
        {
            if (!item.Contains(Consts.Meta))
            {
                _questLine.Add(AssetDatabase.LoadAssetAtPath<QuestEntity>(item));
            }
        }

        foreach (var item in _questLine)
        {
            if (!questLine.Contains(item))
            {
                _newData.Add(item);
            }
        }

        OnQuestLinesHaveDataToUpdate?.Invoke(_newData);
    }

    public void Dispose()
    {
        _controller.OnQuestLinesNeedToUpdate -= SearchFreeQuests;
    }
}
