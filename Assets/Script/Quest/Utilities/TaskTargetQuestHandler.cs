using System.Collections.Generic;
using UnityEngine;

//for quest navigator
public class TaskTargetQuestHandler : MonoBehaviour
{
    public static TaskTargetQuestHandler instance;
    [System.Serializable]
    private struct NPCAndTask
    {
        public ConditionalTask relatedTask;
        public TaskTarget target;
    }
    [SerializeField] private List<NPCAndTask> _NPCQuests;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public TaskTarget getTargetByTask(ConditionalTask conditionalTask)
    {
        foreach (var npc in _NPCQuests)
        {
            if (npc.relatedTask == conditionalTask)
            {
                return npc.target;
            }
        }
        return null;
    }
}
