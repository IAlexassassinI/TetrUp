using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.IO;
using TMPro;

public class ScoreManagerTests
{
    private GameObject testObject;
    private ScoreManager scoreManager;
    private TextMeshProUGUI scoreTextMock;

    [SetUp]
    public void SetUp()
    {
        
        testObject = new GameObject();
        scoreManager = testObject.AddComponent<ScoreManager>();


        scoreTextMock = testObject.AddComponent<TextMeshProUGUI>();
        scoreManager.ScoreText = scoreTextMock;

        scoreManager.Invoke("Awake", 0f);
        
        string scoreFilePath = Path.Combine(Application.persistentDataPath, "scores.json");
        if (File.Exists(scoreFilePath))
        {
            File.Delete(scoreFilePath);
        }
    }

    [TearDown]
    public void TearDown()
    {

        Object.Destroy(testObject);

        string scoreFilePath = Path.Combine(Application.persistentDataPath, "scores.json");
        if (File.Exists(scoreFilePath))
        {
            File.Delete(scoreFilePath);
        }
    }

    [UnityTest]
    public IEnumerator SaveScore_SavesAndLoadsCorrectly()
    {

        scoreManager.SaveScore("Player1", 100);
        scoreManager.SaveScore("Player2", 200);
        scoreManager.SaveScore("Player3", 150);

        scoreManager.LoadScores();

        Assert.AreEqual(3, scoreManager.GetScoresCount());
        Assert.AreEqual("Player2", scoreManager.GetScoreEntryAt(0).PlayerName);
        Assert.AreEqual(200, scoreManager.GetScoreEntryAt(0).Score);
        Assert.AreEqual("Player3", scoreManager.GetScoreEntryAt(1).PlayerName);
        Assert.AreEqual(150, scoreManager.GetScoreEntryAt(1).Score);
        Assert.AreEqual("Player1", scoreManager.GetScoreEntryAt(2).PlayerName);
        Assert.AreEqual(100, scoreManager.GetScoreEntryAt(2).Score);

        yield return null;
    }

    [UnityTest]
    public IEnumerator DisplayScores_UpdatesScoreText()
    {

        scoreManager.SaveScore("Player1", 100);
        scoreManager.SaveScore("Player2", 200);

        scoreManager.LoadScores();
        scoreManager.DisplayScores();

        Assert.AreEqual("Player2\t\t\t\tScore: 200\r\nPlayer1\t\t\t\tScore: 100\r\n", scoreTextMock.text);

        yield return null;
    }

    [UnityTest]
    public IEnumerator LoadScores_LoadsFromFile()
    {

        string scoreFilePath = Path.Combine(Application.persistentDataPath, "scores.json");
        File.WriteAllText(scoreFilePath, "{\"scores\":[{\"PlayerName\":\"PlayerA\",\"Score\":250},{\"PlayerName\":\"PlayerB\",\"Score\":300}]}");

        scoreManager.LoadScores();

        Assert.AreEqual(2, scoreManager.GetScoresCount());
        Assert.AreEqual("PlayerB", scoreManager.GetScoreEntryAt(0).PlayerName);
        Assert.AreEqual(300, scoreManager.GetScoreEntryAt(0).Score);
        Assert.AreEqual("PlayerA", scoreManager.GetScoreEntryAt(1).PlayerName);
        Assert.AreEqual(250, scoreManager.GetScoreEntryAt(1).Score);

        yield return null;
    }
}
