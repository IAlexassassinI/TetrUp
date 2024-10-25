using UnityEngine;
using UnityEngine.UI;
using TMPro; // if using TextMeshPro InputField
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private GameObject panel;

    private uint playerScore;

    public static EndGame Instance { get; private set; } // Instance 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveButton.onClick.AddListener(SaveScore);
        HideWindow();
    }

    public void ShowWindow(uint score)
    {
        if (panel != null)
        {
            PauseMenuManager.Instance.PauseGameNoMenu();
            playerScore = score;
            nameInputField.text = "";
            panel.SetActive(true);
        }
    }

    private void HideWindow()
    {
        panel.SetActive(false);
    }

    private void SaveScore()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            
            ScoreManager.Instance.SaveScore(playerName, (int)playerScore);
            HideWindow();
            PauseMenuManager.Instance.ResumeGameNoMenu();
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            Debug.LogWarning("Player name is empty!");
        }
    }
}
