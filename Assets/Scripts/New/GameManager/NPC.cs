using UnityEngine;

public class NPC
{
    public string name;
    public string animalRace;
    public int moneyAsked;
    public int moneyProfit;
    public int moneyTime;
    public string moneyType;
    public Sprite sprite;

    public NPC(string name, string race, int asked, int profit, int time, string type, Sprite sprite)
    {
        this.name = name;
        this.animalRace = race;
        this.moneyAsked = asked;
        this.moneyProfit = profit;
        this.moneyTime = time;
        this.moneyType = type;
        this.sprite = sprite;
    }
}
