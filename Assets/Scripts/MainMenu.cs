using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }



    public void ShowScore()
    {
        ScoreManager.Instance.SaveScore("Test", 10);
        Debug.Log("Score screen opened.");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}
