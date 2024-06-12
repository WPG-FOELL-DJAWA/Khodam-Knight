using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.QuestJournals;

public class NPCQuestGiver : MonoBehaviour
{
    [SerializeField] private MyCustomQuestDefinition _quest;

    public void startQuest()
    {
        QuestJournalManager.Instance.Quests.Add(_quest);
    }
}
