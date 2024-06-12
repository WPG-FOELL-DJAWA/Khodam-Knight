using System;

public enum Quest
{
    TheWeakest
}

public class QuestEvent
{
    public static event Action TheWeakest;

    public static void RunTheWeakest() => TheWeakest?.Invoke();
}
