using System.Collections.Generic;
using System;

[Serializable]
public class GenericData
{
    public List<string> maleNames;
    public List<string> femaleNames;
    public List<string> reasons;
}

[Serializable]
public class RaceData
{
    public string raceName;

    public List<string> maleNames;
    public List<string> femaleNames;
    public List<string> reasons;

    public float moneyMin;
    public float moneyMax;

    public int profitMin;
    public int profitMax;

    public int daysMin;
    public int daysMax;
}

[Serializable]
public class RaceDatabaseFile
{
    public GenericData generic;
    public List<RaceData> races;
}