using System.Collections.Generic;
using UnityEngine;

public class IconQuestHandler : MonoBehaviour
{
    public static IconQuestHandler instance;
    [System.Serializable]
    private struct iconAndTask
    {
        public ConditionalTask relatedTask;
        public MiniMapIconTask minimapIcon;
    }
    [SerializeField] private List<iconAndTask> _IconQuests;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public MiniMapIconTask getIconByTask(ConditionalTask conditionalTask)
    {
        foreach (var icon in _IconQuests)
        {
            if (icon.relatedTask == conditionalTask)
            {
                return icon.minimapIcon;
            }
        }
        return null;
    }

}
