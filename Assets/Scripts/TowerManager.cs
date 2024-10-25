using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    private float maxHeights = 0;

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

    public void UpdateHeight(float height)
    {
        maxHeights = Mathf.Max(maxHeights, height);
    }

    public float GetMaxTowerHeight()
    {
        return maxHeights;
    }
}
