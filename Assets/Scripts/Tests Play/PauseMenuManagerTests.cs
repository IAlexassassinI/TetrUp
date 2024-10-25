using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using System.Collections;


public class PauseMenuManagerTests : InputTestFixture
{
    private GameObject testObject;
    private PauseMenuManager pauseMenuManager;
    private GameObject pauseMenuPanel;

    [SetUp]
    public void SetUp()
    {
        
        testObject = new GameObject();
        pauseMenuManager = testObject.AddComponent<PauseMenuManager>();

        pauseMenuPanel = new GameObject("PauseMenuPanel");
        pauseMenuManager.pauseMenuPanel = pauseMenuPanel;


        GameObject resumeButtonObject = new GameObject("ResumeButton");
        Button resumeButton = resumeButtonObject.AddComponent<Button>();
        resumeButtonObject.transform.SetParent(pauseMenuPanel.transform);

        GameObject mainMenuButtonObject = new GameObject("MainMenuButton");
        Button mainMenuButton = mainMenuButtonObject.AddComponent<Button>();
        mainMenuButtonObject.transform.SetParent(pauseMenuPanel.transform);


        pauseMenuManager.resumeButton = resumeButton;
        pauseMenuManager.mainMenuButton = mainMenuButton;

        pauseMenuManager.Invoke("Start", 0f);

        pauseMenuManager.pauseMenuPanel.SetActive(false);

        mainMenuButton.onClick.AddListener(() => {

            Debug.Log("Scene should load: MenuScene");
        });
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testObject);
        Object.Destroy(pauseMenuPanel);
    }

    [UnityTest]
    public IEnumerator PauseGame_SetsTimeScaleAndActivatesPanel()
    {
       
        pauseMenuManager.PauseGame();

        
        Assert.IsTrue(pauseMenuManager.GetPauseStatus());
        Assert.AreEqual(0f, Time.timeScale);
        Assert.IsTrue(pauseMenuManager.pauseMenuPanel.activeSelf);

        yield return null; 
    }

    [UnityTest]
    public IEnumerator ResumeGame_ResetsTimeScaleAndDeactivatesPanel()
    {
        
        pauseMenuManager.PauseGame();

        pauseMenuManager.ResumeGame();

        Assert.IsFalse(pauseMenuManager.GetPauseStatus());
        Assert.AreEqual(1f, Time.timeScale);
        Assert.IsFalse(pauseMenuManager.pauseMenuPanel.activeSelf);

        yield return null;
    }

    [UnityTest]
    public IEnumerator PauseMenuButtons_WorkAsExpected()
    {

        pauseMenuManager.PauseGame();

        pauseMenuManager.resumeButton.onClick.Invoke();

        Assert.IsFalse(pauseMenuManager.GetPauseStatus());

        pauseMenuManager.mainMenuButton.onClick.Invoke();

        Assert.AreEqual("MenuScene", SceneManager.GetActiveScene().name);

        yield return null;
    }


}
