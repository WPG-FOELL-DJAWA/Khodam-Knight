using CleverCrow.Fluid.QuestJournals.Tasks;
using UnityEngine;

public class MiniMapIconTask : MiniMapIcon
{
    [Header("Quest")]
    [SerializeField] private ConditionalTask _relatedTask;

    private void Start()
    {

        ITaskInstance activeTask = HUDHandler.instance.questHandler.getActiveQuestTaskInstance();

        if (activeTask.Definition as ConditionalTask == _relatedTask)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
