using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.QuestJournals;

public class SpawnQuestItem : ActionDataBase
{
    [Header("Need more invoke if add more quest")]
    [SerializeField] MyCustomQuestDefinition _relatedQuest;
    [SerializeField] private Quest questName;
    public override void OnInit(IDialogueController dialogue)
    {
        // Run the first time the action is triggered
    }

    public override void OnStart()
    {
        // Runs when the action begins triggering
    }

    public override ActionStatus OnUpdate()
    {
        // Runs when the action begins triggering

        // Return continue to span multiple frames
        return ActionStatus.Success;
    }

    public override void OnExit()
    {
        // Runs when the actions `OnUpdate()` returns `ActionStatus.Success`
        var quest = QuestJournalManager.Instance.Quests.Get(_relatedQuest);
        HUDHandler.instance.questHandler.printQuestDetails.SetQuest(quest);

        if (questName == Quest.TheWeakest)
            QuestEvent.RunTheWeakest();
    }

    public override void OnReset()
    {
        // Runs after a node has fully run through the start, update, and exit cycle
    }
}
