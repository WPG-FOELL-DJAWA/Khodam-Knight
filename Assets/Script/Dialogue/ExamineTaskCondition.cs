using UnityEngine;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Nodes;
using CleverCrow.Fluid.QuestJournals;

public class ExamineTaskCondition : ConditionDataBase
{
    [SerializeField] private MyCustomQuestDefinition _relatedQuest;
    [SerializeField] private ConditionalTask _relatedTask;
    public override void OnInit(IDialogueController dialogue)
    {
        // Triggered on first time setup
    }

    public override bool OnGetIsValid(INode parent)
    {
        var questInstance = QuestJournalManager.Instance.Quests.Get(_relatedQuest);
        var activeTask = questInstance.ActiveTask.Definition as ConditionalTask;

        if (activeTask == _relatedTask)
            return _relatedTask.examineCondition();
        else
            return false;
    }
}
