using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionEntityScriptable", menuName = "ScriptableObjects/ConditionEntity")]
public class ConditionEntity : ScriptableObject
{
    public ConditionTypes Condition;
    public int MinLevel;
    public int MinEnergy;
    public int QuestID;
}

[CustomEditor(typeof(ConditionEntity))]
public class ConditionEntityEditor : Editor
{
    ConditionEntity _entity;

    public void OnEnable()
    {
        _entity = (ConditionEntity)target;
    }

    public override void OnInspectorGUI()
    {
        _entity.Condition = (ConditionTypes)EditorGUILayout.EnumPopup("Choose condition type", _entity.Condition);

        switch (_entity.Condition)
        {
            case ConditionTypes.Level:
                _entity.MinLevel = EditorGUILayout.IntField("Min level", _entity.MinLevel);
                break;
            case ConditionTypes.MinigameComplete:
                _entity.QuestID = EditorGUILayout.IntField("Choose quest ID", _entity.QuestID);
                break;
        }
    }
}