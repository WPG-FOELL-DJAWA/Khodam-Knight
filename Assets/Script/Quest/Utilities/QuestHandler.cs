using UnityEngine;
using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.QuestJournals.Quests;
using CleverCrow.Fluid.QuestJournals.Tasks;
using CleverCrow.Fluid.QuestJournals.Implement;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] private GameObject _questJournalObject;
    public PrintQuestDetails printQuestDetails;
    private MiniMapIconTask _miniMapIconTask;
    private TaskTarget _TaskTarget;
    private IQuestInstance _currentQuestInstance;
    private ITaskInstance _currentTaskInstance;

    public void openQuestJournal()
    {
        _questJournalObject.SetActive(true);
    }

    public void closeQuestJournal()
    {
        _questJournalObject.SetActive(false);
    }

    public void setSelectedQuest(MyCustomQuestDefinition _selectedQuest)
    {
        _currentQuestInstance = QuestJournalManager.Instance.Quests.Get(_selectedQuest);
        _currentTaskInstance = _currentQuestInstance.ActiveTask as ITaskInstance;

        _miniMapIconTask = IconQuestHandler.instance.getIconByTask(_currentTaskInstance.Definition as ConditionalTask);
        _TaskTarget = TaskTargetQuestHandler.instance.getTargetByTask(_currentTaskInstance.Definition as ConditionalTask);
    }

    public ITaskInstance getActiveQuestTaskInstance()
    {
        return _currentTaskInstance;
    }

    public MiniMapIconTask getCurrentMiniMapIconTask()
    {
        return _miniMapIconTask;
    }

    public TaskTarget getCurrentTaskTarget()
    {
        return _TaskTarget;
    }
}