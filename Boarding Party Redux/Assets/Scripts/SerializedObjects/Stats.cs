using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    health,
    damage,
    damageMultiplier,
    speed,
    detectionRange,
    firingSpeed,
    score
}

[Serializable] public class StatList
{
    public List<StatValue> statValues;
}

[Serializable] public class StatValue
{
    public StatType stat;
    public float value;
}

[Serializable]
[CreateAssetMenu(menuName = "Stat set")]
public class Stats : ScriptableObject
{
    public StatList baseStatList;
    public StatList maxStatList;
    // public StatDictionary testDictionary = new StatDictionary();

    public Dictionary<StatType, float> stat = new Dictionary<StatType, float> {};
    public Dictionary<StatType, float> baseStat = new Dictionary<StatType, float> {};
    public Dictionary<StatType, float> maxStat = new Dictionary<StatType, float> {};


    public void SetStats()
    {
        stat = NewDictionary(baseStatList.statValues);
        baseStat = NewDictionary(baseStatList.statValues);
        maxStat = NewDictionary(maxStatList.statValues);
    }

    public Dictionary<StatType, float> NewDictionary(List<StatValue> statValues)
    {
        Dictionary<StatType, float> dict = new Dictionary<StatType, float> {};

        foreach (StatType newStat in System.Enum.GetValues(typeof(StatType)))
        {
            dict.Add(newStat, 0);
        }

        for (int i = 0; i < statValues.Count; i++)
        {
            dict[statValues[i].stat] = statValues[i].value;
        }

        return dict;
    }

    public void ResetStat(StatType statType)
    {
        stat[statType] = baseStat[statType];
    }

    public void MaximizeStat(StatType statType)
    {
        stat[statType] = maxStat[statType];
    }
}