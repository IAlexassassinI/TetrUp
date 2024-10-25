using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    private uint maxHeight = 0;
    private uint currentHeight = 0;

    [SerializeField]
    private Transform maxHeightText;
    private TextMeshProUGUI maxHeightText_T;


    [SerializeField]
    private Transform currentHealthText;
    private TextMeshProUGUI currentHealthText_T;
    private int currentHealth = 3;
    private int maxHealth = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            maxHeightText_T = maxHeightText.GetComponent<TextMeshProUGUI>();
            currentHealthText_T = currentHealthText.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public uint UpdateMaxHeight(uint height)
    {
        currentHeight = height;
        maxHeight = Math.Max(maxHeight, height);
        UpdateMaxHeightText();
        Debug.Log("Max height:"+maxHeight);
        return maxHeight;
    }

    public uint GetMaxTowerHeight()
    {
        return maxHeight;
    }

    public uint GetCurrentTowerHeight()
    {
        return currentHeight;
    }

    private void UpdateMaxHeightText() {
        maxHeightText_T.text = "Max height: "+GetMaxTowerHeight();
    }

    public int UpdateCurrentHealth(int delta) {
        currentHealth += delta;
        if (currentHealth > maxHealth) { 
            currentHealth = maxHealth;
        }
        TryTriggerEndGame();
        UpdateCurrentHealthText();
        return currentHealth;
    }

    private void TryTriggerEndGame() {
        if (currentHealth <= 0) {
            Debug.Log("END GAME");
            EndGame.Instance.ShowWindow(currentHeight);
        }
    }

    private int GetCurretnHealth() {
        return currentHealth;
    }

    private void UpdateCurrentHealthText() {
        currentHealthText_T.text =  "Left health: " + GetCurretnHealth();
    }
}
