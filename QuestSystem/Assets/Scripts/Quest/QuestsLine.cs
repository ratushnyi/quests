using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestsLineScriptable", menuName = "ScriptableObjects/QuestsLineItem")]
public class QuestsLine : ScriptableObject
{
    public List<QuestEntity> questLine;
}
