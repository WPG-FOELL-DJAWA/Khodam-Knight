
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HealthMana : MonoBehaviour
{
    [SerializeField] private Slider _hp;
    [SerializeField] private Slider _mp1;
    [SerializeField] private Slider _mp2;
    private Statistic playerStatistic;

    public void updateHealth(Statistic statistic)
    {
        playerStatistic = statistic;
        _hp.maxValue = statistic.statisticData.health;
        _hp.value = statistic.currentHealth;
    }

    private void LateUpdate()
    {

        // noted : perlu perbaikan agar dia tidak terpanggil saat player mati
        if (playerStatistic)
        {
            _hp.value = playerStatistic.currentHealth;
        }
    }

}
