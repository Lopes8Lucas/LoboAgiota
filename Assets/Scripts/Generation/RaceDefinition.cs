public class RaceDefinition
{
    public string race;
    public string name;
    public string reason;
    public float moneyNeeded;
    public int profitPercentage;
    public int daysToGive;
    public bool isHonest;
    public bool isMale;

    public RaceDefinition()
    {
        var db = RaceDatabase.Instance;
        RaceData raceData = db.GetRandomRace();
        race = raceData.raceName;
        isMale = UnityEngine.Random.value > 0.5f;

        name = db.GetRandomName(raceData, isMale);
        reason = db.GetRandomReason(raceData);
        moneyNeeded = UnityEngine.Random.Range(raceData.moneyMin, raceData.moneyMax);
        profitPercentage = UnityEngine.Random.Range(raceData.profitMin, raceData.profitMax + 1);
        daysToGive = UnityEngine.Random.Range(raceData.daysMin, raceData.daysMax + 1);
        isHonest = UnityEngine.Random.value > 0.5f;
    }
}