using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI ScoreText;

    public List<ScoreEntry> scores = new List<ScoreEntry>();
    public static ScoreManager Instance { get; private set; }

    private string scoreFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            scoreFilePath = Path.Combine(Application.persistentDataPath, "scores.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScores();
        if (ScoreText != null)
        {
            DisplayScores();
        }
    }

    public void LoadScores()
    {
        scores.Clear();

        if (File.Exists(scoreFilePath))
        {
            string json = File.ReadAllText(scoreFilePath);
            scores = JsonUtility.FromJson<ScoreList>(json)?.scores ?? new List<ScoreEntry>();
            scores.Sort((x, y) => y.Score.CompareTo(x.Score));
        }
    }

    public void SaveScore(string playerName, int score)
    {
        scores.Add(new ScoreEntry(playerName, score));
        scores.Sort((x, y) => y.Score.CompareTo(x.Score));

        ScoreList scoreList = new ScoreList { scores = scores };
        string json = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(scoreFilePath, json);
    }

    public void DisplayScores()
    {
        StringBuilder scoreTextBuilder = new StringBuilder();

        int maxEntries = Mathf.Min(10, scores.Count);
        for (int i = 0; i < maxEntries; i++)
        {
            var scoreEntry = scores[i];
            scoreTextBuilder.AppendLine($"{scoreEntry.PlayerName}\t\t\t\tScore: {scoreEntry.Score}");
        }

        ScoreText.text = scoreTextBuilder.ToString();
    }

    [System.Serializable]
    public class ScoreEntry
    {
        public string PlayerName;
        public int Score;

        public ScoreEntry(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
        }
    }

    [System.Serializable]
    public class ScoreList
    {
        public List<ScoreEntry> scores;
    }

    public int GetScoresCount() => scores.Count;

    public ScoreEntry GetScoreEntryAt(int index) => scores[index];



}
