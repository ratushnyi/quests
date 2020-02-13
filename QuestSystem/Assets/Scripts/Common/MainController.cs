using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MainController : MonoBehaviour, IMainController
{
    public event Action<List<QuestEntity>> OnQuestLinesNeedToUpdate;

    private UserData _data;
    private List<QuestEntity> _questLine;
    private GameCycleManager _gameCycleManager;

    void Start()
    {
        _questLine = AssetDatabase.LoadAssetAtPath<QuestsLine>(Consts.QuestLinePath).questLine;

        _gameCycleManager = new GameCycleManager(this);
        _gameCycleManager.OnQuestLinesHaveDataToUpdate += AddDataToQuestLine;

        try
        {
            _data = SaveLoadSystem.Load<UserData>();

            if (_data == null)
            {
                return;
            }

            Debug.Log($"Hallo! Old user!");

            GetStats();
        }
        catch (FileNotFoundException e)
        {
            _data = new UserData();

            Debug.Log($"Hallo! New user!");

            GetStats();

            SaveLoadSystem.Save(_data);
        }
    }

    private void AddDataToQuestLine(List<QuestEntity> data)
    {
        _questLine.AddRange(data);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _data.Energy++;

            Debug.Log($"You get 1 point of energy, you current level of energy is {_data.Energy}");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            try
            {
                var availableQuest = _questLine.First(t => !t.IsComplete);

                Debug.Log($"You try to pass the {availableQuest.Name}");

                StartCoroutine(TestCoroutine(availableQuest));
            }
            catch (InvalidOperationException e)
            {
                Debug.Log($"You are already done all quests");
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            GetStats();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLoadSystem.Save(_data);

            OnQuestLinesNeedToUpdate?.Invoke(_questLine);
        }
    }

    IEnumerator TestCoroutine(QuestEntity quest)
    {
        var (isPassed, info) = quest.AttemptToPass(_data);

        yield return new WaitForSeconds(1f);

        Debug.Log("Please wait"); 

        yield return new WaitForSeconds(2f);

        Debug.Log(info);

        if (isPassed)
        {
            quest.IsComplete = true;

            _data.CompleteQuest(quest);

            GetStats();
        }
    }

    private void GetStats()
    {
        Debug.Log($"Your level is {_data.Rewards.Level}, " +
                  $"you have {_data.Rewards.Money} coins and you have {_data.Energy} energy points");

        if (_data.QuestsData.Count > 0)
        {
            Debug.Log($"You are already complete");

            foreach (var item in _data.QuestsData)
            {
                Debug.Log(item.ToString());
            }
        }
    }

    void OnDisable()
    {
        SaveLoadSystem.Save(_data);

        _gameCycleManager.Dispose();
        _gameCycleManager.OnQuestLinesHaveDataToUpdate -= AddDataToQuestLine;
    }
}