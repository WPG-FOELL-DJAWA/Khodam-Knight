[System.Serializable]
public class StatisticData
{
    public string name;
    public int level;
    public int health = 100;
    public int attack = 10;
    public int mana;
    public int defense;
    public int critDMG;
    public int critRate;
    public int accuracy;
    public int evasion;
    public float constant = .95f;

    public int damageMitigation = 0;
    public int defenseMitigation = 0;
    public int resistanceMitigation = 0;
}
