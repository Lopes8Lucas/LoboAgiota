using System.Collections.Generic;
using UnityEngine;

public class RaceDatabase : MonoBehaviour
{
    public static RaceDatabase Instance;

    public RaceDatabaseFile data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("RaceData");
        data = JsonUtility.FromJson<RaceDatabaseFile>(jsonFile.text);
    }

    public RaceData GetRandomRace()
    {
        return data.races[UnityEngine.Random.Range(0, data.races.Count)];
    }

    public string GetRandomName(RaceData race, bool isMale)
    {
        var specificList = isMale ? race.maleNames : race.femaleNames;
        var genericList = isMale ? data.generic.maleNames : data.generic.femaleNames;

        List<string> final = new List<string>();
        if (specificList != null) final.AddRange(specificList);
        if (genericList != null) final.AddRange(genericList);

        return final.Count > 0 ? final[UnityEngine.Random.Range(0, final.Count)] : "Unnamed";
    }

    public string GetRandomReason(RaceData race)
    {
        List<string> final = new List<string>();
        if (race.reasons != null) final.AddRange(race.reasons);
        if (data.generic.reasons != null) final.AddRange(data.generic.reasons);

        return final.Count > 0 ? final[UnityEngine.Random.Range(0, final.Count)] : "Unknown reason";
    }
}
