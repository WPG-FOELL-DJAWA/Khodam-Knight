using System.Linq;
using CleverCrow.Fluid.QuestJournals.Quests;
using CleverCrow.Fluid.QuestJournals.Tasks;
using UnityEngine;
using CleverCrow.Fluid.Databases;


namespace CleverCrow.Fluid.QuestJournals.Implement
{
    public class PrintQuestDetails : MonoBehaviour
    {
        [Header("Quest")]

        [SerializeField]
        private GenericText _questTitle;


        [SerializeField]
        private GenericText _questStatus;

        [SerializeField]
        private GenericText _questDescription;

        [Header("Active Task")]
        [SerializeField] private GenericButton _navigationButton;

        [SerializeField]
        private GenericText _taskTitle;

        [SerializeField]
        private GenericText _taskStatus;

        [SerializeField]
        private GenericText _taskDescription;

        [Header("Task List")]

        [SerializeField]
        private GenericText _listItemPrefab;
        [SerializeField] GenericText _taskConditionPrefab;

        [SerializeField]
        private RectTransform _taskListOutput;
        [SerializeField] RectTransform _taskConditionOutput;

        [SerializeField]
        private bool _hideEmptyTasks = true;

        [SerializeField]
        private bool _printTaskDetails;

        public void SetQuest(IQuestInstance quest)
        {
            RefreshDisplay(quest);
            QuestSummary.instance.SetQuest(quest);
            HUDHandler.instance.questHandler.setSelectedQuest(quest.Definition as MyCustomQuestDefinition);
        }

        private void RefreshDisplay(IQuestInstance quest)
        {
            UpdateQuest(quest);
            UpdateTask(quest);
            setTaskCondition(quest);
            setNavigationButton(quest);
            UpdateTaskList(quest);
        }

        private void UpdateTaskList(IQuestInstance quest)
        {
            foreach (Transform t in _taskListOutput)
            {
                Destroy(t.gameObject);
            }

            quest.Tasks.ToList().ForEach(task =>
            {
                if (_hideEmptyTasks && task.Status == TaskStatus.None) return;
                var listItem = Instantiate(_listItemPrefab, _taskListOutput);

                var title = task.Title;
                if (_printTaskDetails)
                {
                    title += $" {task.Status.ToString()}";
                    if (quest.ActiveTask == task) title = $"-> {title}";
                }

                listItem.SetText(title);

            });

        }

        private void setTaskCondition(IQuestInstance quest)
        {
            var task = quest.ActiveTask;
            var t = task.Definition as ConditionalTask;

            var db = new DatabaseInstance();

            foreach (Transform ta in _taskConditionOutput)
            {
                Destroy(ta.gameObject);
            }

            if (t.isNeedKeyValueDesc)
            {
                foreach (var value in t.condition)
                {
                    var myKey = value.keyValue.Key;
                    var myVal = db.Ints.Get(myKey);

                    var taskCondition = Instantiate(_taskConditionPrefab, _taskConditionOutput);
                    taskCondition.SetText(value.keyValueName + " (" + myVal.ToString() + "/" + value.keyValueTarget + ")");
                }
            }

        }

        private void setNavigationButton(IQuestInstance quest)
        {
            ConditionalTask currentTask = quest.ActiveTask.Definition as ConditionalTask;
            if (currentTask.isNeedNavigationButton)
            {
                _navigationButton.gameObject.SetActive(true);
                _navigationButton.BindButton(() => clickNavigation(currentTask));
                _navigationButton.SetText("Navigation");
            }
            else
            {
                _navigationButton.gameObject.SetActive(false);
            }
        }

        private void UpdateTask(IQuestInstance quest)
        {
            _taskTitle?.SetText(quest.ActiveTask.Title);
            _taskStatus?.SetText(quest.ActiveTask.Status.ToString());
            _taskDescription?.SetText(quest.ActiveTask.Description);
        }

        private void UpdateQuest(IQuestInstance quest)
        {
            _questTitle?.SetText(quest.Title);
            _questStatus?.SetText(quest.Status.ToString());
            _questDescription?.SetText(quest.Description);
        }

        private void clickNavigation(ConditionalTask task)
        {
            MiniMapIconTask miniMapIconTask = HUDHandler.instance.questHandler.getCurrentMiniMapIconTask();

            if (miniMapIconTask == null)
                Debug.LogError("Icon task tidak terdeteksi");

            HUDHandler.instance.interactiveMiniMap.autoNavigation(task, miniMapIconTask);

            HUDHandler.instance.closeQuest();
            HUDHandler.instance.openMiniMap();


            if (RoamingMapData.instance.getMapName() == task.getMapTarget())
            {
                TaskTarget taskTarget = HUDHandler.instance.questHandler.getCurrentTaskTarget();
                if (taskTarget == null)
                    Debug.LogError("task target tidak terdeteksi");
                LineNavigation.instance.startDrawPathToTarget(taskTarget.gameObject);
            }
        }
    }
}
