using UnityEngine;
using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.Databases;


[RequireComponent(typeof(BoxCollider))]
public class ItemQuest : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private MyCustomQuestDefinition _relatedQuest;
    [SerializeField] private ConditionalTask _relatedTask;
    [SerializeField] KeyValueDefinitionInt keyValueQuest;
    [SerializeField] private string itemId;
    [SerializeField] private GameObject itemInWorld;
    [SerializeField] private Collider trigger;

    private bool onTakeItem = false;

    private void OnEnable()
    {
        itemInWorld.SetActive(false);
        trigger.enabled = false;
        if (quest == Quest.TheWeakest)
            QuestEvent.TheWeakest += SpawnItem;
    }

    private void SpawnItem()
    {
        var questInstance = QuestJournalManager.Instance.Quests.Get(_relatedQuest);
        var activeTask = questInstance.ActiveTask.Definition as ConditionalTask;

        if (activeTask == _relatedTask)
        {
            itemInWorld.SetActive(true);
            trigger.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HUDHandler.instance.roamingUIHandler.showInteractUI();
            onTakeItem = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CharacterInput.instance.actionTrigger && !onTakeItem)
        {
            var db = new DatabaseInstance();
            var myVal = db.Ints.Get(keyValueQuest.key);
            db.Ints.Set(keyValueQuest.key, myVal + 1);
            HUDHandler.instance.inventoryHandler.AddItem(itemId, 1);

            itemInWorld.SetActive(false);

            if (quest == Quest.TheWeakest)
                QuestEvent.TheWeakest -= SpawnItem;
            onTakeItem = true;
            Debug.Log(db.Ints.Get(keyValueQuest.key));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HUDHandler.instance.roamingUIHandler.hideInteractUI();
            onTakeItem = false;
        }
    }
}
