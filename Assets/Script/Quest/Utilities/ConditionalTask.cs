using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.QuestJournals.Tasks;
using UnityEngine;
using CleverCrow.Fluid.Databases;
using System.Collections.Generic;

[CreateMenu("Conditional Task")]
public class ConditionalTask : TaskDefinitionBase
{
    [SerializeField] private MapName _targetMap;
    [Space]

    [Header("UI")]
    public bool isNeedKeyValueDesc = false;
    public bool isNeedNavigationButton = false;

    [System.Serializable]
    public struct Condition
    {
        public string keyValueName;
        public KeyValueDefinitionInt keyValue;
        public int keyValueTarget;
    }
    [Header("Finish Condition")]
    [SerializeField] private List<Condition> _condition;
    public List<Condition> condition { get { return _condition; } }


    private List<bool> _examineCondition;

    public bool examineCondition()
    {
        var db = new DatabaseInstance();

        _examineCondition.Clear();

        //examine all condition
        foreach (var cond in _condition)
        {
            string Key = cond.keyValue.key;
            int val = db.Ints.Get(Key);

            if (val >= cond.keyValueTarget)
            {
                _examineCondition.Add(true);
            }
            else
            {
                _examineCondition.Add(false);
            }
        }


        //count how many true condition
        int totalTrue = 0;
        foreach (var cond in _examineCondition)
        {
            if (cond)
            {
                totalTrue++;
            }
        }

        if (totalTrue >= _examineCondition.Count)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void resetKeyValue()
    {
        var db = new DatabaseInstance();

        foreach (var cond in _condition)
        {
            string Key = cond.keyValue.key;
            db.Ints.Set(Key, 0);
        }
    }

    public MapName getMapTarget()
    {
        return _targetMap;
    }
}