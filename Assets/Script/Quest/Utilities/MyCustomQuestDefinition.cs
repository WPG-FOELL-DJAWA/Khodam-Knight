using CleverCrow.Fluid.QuestJournals;
using CleverCrow.Fluid.QuestJournals.Quests;
using UnityEngine;

[CreateMenu("My Custom Quest")]
public class MyCustomQuestDefinition : QuestDefinitionBase
{
    public QuestType questType;

    [Header("Reward")]
    public string Reward; // noted

    public void CustomMethodsGoHere()
    {
    }
}