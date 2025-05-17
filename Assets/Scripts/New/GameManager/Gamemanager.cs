using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameObject analysisTable;

    [Header("Global Game Settings")]
    public int totalNPCs = 10;
    public bool persistentBetweenScenes = true;

    [Header("NPC Display")]
    public SpriteRenderer npcRenderer; // Or change to Image if using UI
    public GameObject npcDisplayObject; // Optional: The whole NPC GameObject

    private Queue<NPC> npcQueue = new Queue<NPC>();
    private NPC currentNPC;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (persistentBetweenScenes)
            DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindStartButton();
        FindAnalysisTable();
    }

    public void FindStartButton()
    {
        GameObject buttonObj = GameObject.Find("StartButton");

        if (buttonObj != null && buttonObj.TryGetComponent(out Button button))
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                StartGame();
                buttonObj.SetActive(false);
            });
        }
    }

    public void FindAnalysisTable()
    {
        analysisTable = GameObject.Find("AnalysisTable");
        analysisTable.SetActive(false);
    }

    public void StartGame()
    {
        GenerateNPCs(totalNPCs);
        ShowNextNPC();
        OpenAnalysisTable();
    }

    public void GenerateNPCs(int count)
    {
        npcQueue.Clear();

        foreach (var race in races)
        {
            for (int i = 0; i < count / races.Length; i++)
            {
                string name = GetRandom(race.possibleNames);
                string type = GetRandom(race.moneyTypes);
                Sprite sprite = GetRandom(race.possibleSprites);

                int asked = Random.Range(race.moneyAskedRange.x, race.moneyAskedRange.y + 1);
                int profit = Random.Range(race.moneyProfitRange.x, race.moneyProfitRange.y + 1);
                int time = Random.Range(race.moneyTimeRange.x, race.moneyTimeRange.y + 1);

                NPC newNpc = new NPC(name, race.raceName, asked, profit, time, type, sprite);
                npcQueue.Enqueue(newNpc);
            }
        }

        Debug.Log($"{npcQueue.Count} NPCs queued.");
    }

    public void ShowNextNPC()
    {
        if (npcQueue.Count == 0)
        {
            Debug.Log("No more NPCs in queue.");
            return;
        }

        currentNPC = npcQueue.Dequeue();
        UpdateNPCDisplay(currentNPC);
    }

    public void OpenAnalysisTable()
    {
        analysisTable.SetActive(true);
    }

    public void UpdateNPCDisplay(NPC npc)
    {
        if (npcRenderer != null)
        {
            npcRenderer.sprite = npc.sprite;
        }

        Debug.Log($"Now showing NPC: {npc.name}, asks for {npc.moneyAsked} {npc.moneyType}");
    }

    public NPC GetCurrentNPC()
    {
        return currentNPC;
    }

    // Helper for random selection
    private T GetRandom<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    // YouÅfll need to assign RaceDefinition[] in the Inspector
    [System.Serializable]
    public class RaceDefinition
    {
        public string raceName;
        public string[] possibleNames;
        public string[] moneyTypes;
        public Sprite[] possibleSprites;
        public Vector2Int moneyAskedRange;
        public Vector2Int moneyProfitRange;
        public Vector2Int moneyTimeRange;
    }

    public RaceDefinition[] races;
}
