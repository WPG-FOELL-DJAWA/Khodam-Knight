using UnityEngine;
using CleverCrow.Fluid.QuestJournals.Quests;
using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.Databases;
using UnityEngine.UI;


namespace CleverCrow.Fluid.QuestJournals.Implement
{
    public class QuestSummary : MonoBehaviour
    {
        public static QuestSummary instance;
        [SerializeField] private GenericButton _navigationButton;
        [SerializeField] private GenericText _questTitle;
        [SerializeField] private GenericText _activeTask;
        [SerializeField] private GenericText _activeTaskDesc;

        [Header("Utilities")]
        [SerializeField] private GenericText _taskConditionPrefab;
        [SerializeField] private VerticalLayoutGroup _TaskGroup;

        [SerializeField] RectTransform _taskConditionOutput;

        private ConditionalTask _currentQuest;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            _navigationButton.BindButton(() => clickNavigation());

            var allQuests = QuestJournalManager.Instance.Quests.GetAll();
            if (allQuests.Count > 0)
            {
                SetQuest(allQuests[0]);
                HUDHandler.instance.questHandler.setSelectedQuest(allQuests[0].Definition as MyCustomQuestDefinition);
            }
            else
                gameObject.SetActive(false);

            //initialize default quest

            Canvas.ForceUpdateCanvases();
            _TaskGroup.SetLayoutVertical();
        }

        private void clickNavigation()
        {
            if (RoamingMapData.instance.getMapName() != _currentQuest.getMapTarget())
            {
                MiniMapIconTask miniMapIconTask = HUDHandler.instance.questHandler.getCurrentMiniMapIconTask();
                HUDHandler.instance.interactiveMiniMap.autoNavigation(_currentQuest, miniMapIconTask);
                HUDHandler.instance.openMiniMap();
            }
            else
            {
                if (!LineNavigation.instance.navigationIsActive)
                {
                    TaskTarget taskTarget = HUDHandler.instance.questHandler.getCurrentTaskTarget();
                    if (taskTarget == null)
                        Debug.LogError("task target tidak terdeteksi");
                    else
                        LineNavigation.instance.startDrawPathToTarget(taskTarget.gameObject);
                }
                else
                {
                    LineNavigation.instance.stopDrawPathToTarget();
                }
            }
        }


        public void SetQuest(IQuestInstance quest)
        {
            _currentQuest = quest.ActiveTask.Definition as ConditionalTask;
            RefreshDisplay(quest);
        }

        public void RefreshDisplay(IQuestInstance quest)
        {
            _questTitle.SetText(quest.Title);
            _activeTask.SetText(quest.ActiveTask.Title);
            setTaskCondition(quest);
            _activeTaskDesc.SetText(quest.ActiveTask.Description);
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


            Canvas.ForceUpdateCanvases();
            _TaskGroup.SetLayoutVertical();
        }
    }
}