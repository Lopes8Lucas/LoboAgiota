using UnityEngine;
using System.Collections.Generic;

public class NPCGenerator : MonoBehaviour
{
    [System.Serializable]
    public class RaceDefinition
    {
        public string raceName;
        public string[] possibleNames;
        public string[] moneyTypes;
        public Sprite[] possibleSprites;

        [Header("Money Ranges (int)")]
        public Vector2Int moneyAskedRange;   // e.g. (50, 200)
        public Vector2Int moneyProfitRange;  // e.g. (10, 80)
        public Vector2Int moneyTimeRange;    // e.g. (1, 7) days or hours
    }

    [Header("Race Definitions")]
    public RaceDefinition[] races;

    private Dictionary<string, List<Dictionary<string, object>>> npcData =
        new Dictionary<string, List<Dictionary<string, object>>>();

    private Dictionary<string, Sprite> npcSpritesMap =
        new Dictionary<string, Sprite>();

    void Start()
    {
        GenerateNPCs(10);
    }

    void GenerateNPCs(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            RaceDefinition race = GetRandom(races);
            string npcKey = $"NPC_{i}";

            var npcEntry = new Dictionary<string, object>
        {
            { "name", GetRandom(race.possibleNames) },
            { "animalRace", race.raceName },
            { "moneyAsked", Random.Range(race.moneyAskedRange.x, race.moneyAskedRange.y + 1) },
            { "moneyProfit", Random.Range(race.moneyProfitRange.x, race.moneyProfitRange.y + 1) },
            { "moneyTime", Random.Range(race.moneyTimeRange.x, race.moneyTimeRange.y + 1) },
            { "moneyType", GetRandom(race.moneyTypes) }
        };

            npcData[npcKey] = new List<Dictionary<string, object>> { npcEntry };
            npcSpritesMap[npcKey] = GetRandom(race.possibleSprites);
        }

        PrintAllNPCs();
    }


    T GetRandom<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    void PrintAllNPCs()
    {
        foreach (var npc in npcData)
        {
            Debug.Log($"{npc.Key}:");

            foreach (var entry in npc.Value)
            {
                string entryInfo = "";
                foreach (var kvp in entry)
                {
                    entryInfo += $"{kvp.Key}={kvp.Value}, ";
                }
                Debug.Log("    " + entryInfo.TrimEnd(',', ' '));
            }

            Debug.Log($"Sprite: {npcSpritesMap[npc.Key].name}");
        }
    }
}
