using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues;
using UnityEngine;
using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.QuestJournals.Implement;

public class NextQuestTask : ActionDataBase
{
    [SerializeField] MyCustomQuestDefinition _relatedQuest;
    [SerializeField] private ConditionalTask _currentTask;
    public override void OnInit(IDialogueController dialogue)
    {
        // Run the first time the action is triggered
    }

    public override void OnStart()
    {
        // Runs when the action begins triggering
        _currentTask.resetKeyValue();
        var quest = QuestJournalManager.Instance.Quests.Get(_relatedQuest);

        if (LineNavigation.instance.navigationIsActive)
            LineNavigation.instance.stopDrawPathToTarget();
        quest.Next();
        HUDHandler.instance.questHandler.printQuestDetails.SetQuest(quest);
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

    }

    public override void OnReset()
    {
        // Runs after a node has fully run through the start, update, and exit cycle
    }
}
