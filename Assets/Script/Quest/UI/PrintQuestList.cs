using CleverCrow.Fluid.QuestJournals.Quests;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.Fluid.QuestJournals.Implement
{
    public class PrintQuestList : MonoBehaviour
    {
        [SerializeField] private GenericButton _buttonPrefab;
        [SerializeField] private PrintQuestDetails _printQuest;
        [SerializeField] private UnityEvent<IQuestInstance> _onQuestClick;
        [SerializeField] private QuestType _questType;
        [SerializeField] private QuestStatus _questsStatus;

        private void Start()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }


            var allQuests = QuestJournalManager.Instance.Quests.GetAll();
            allQuests.ForEach(quest =>
            {
                // get my custom quest
                var q = quest.Definition as MyCustomQuestDefinition;

                if (quest.Status != _questsStatus || q.questType != _questType)
                {
                    return;
                }

                var btn = Instantiate(_buttonPrefab, transform);
                btn.BindButton(() => ClickQuest(quest));
                btn.SetText(quest.Title);
            });

            if (allQuests.Count > 0)
                _printQuest.SetQuest(allQuests[0]);
        }

        private void ClickQuest(IQuestInstance quest)
        {
            _onQuestClick.Invoke(quest);
        }
    }
}
