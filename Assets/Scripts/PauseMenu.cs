using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuPanel; 
    [SerializeField] public Button resumeButton;
    [SerializeField] public Button mainMenuButton;

    private bool isPaused = false;

    public static PauseMenuManager Instance { get; private set; } 

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

    public bool GetPauseStatus() { 
        return isPaused;
    }

    private void Start()
    {
        
        pauseMenuPanel.SetActive(false);

        
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("PPP");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    public void PauseGameNoMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        
    }

    public void ResumeGameNoMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}
