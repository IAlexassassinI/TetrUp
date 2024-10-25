using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    private string[] shapes = { "I", "O", "T", "L", "J", "S", "Z" };
    private TetrominoFactory tetrominoFactory;

    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private Transform platformPosition;

    public static bool spawnedAndActive = false;

    private CameraRiser cameraRiser;
    private CameraRiser spawnerRiser;

    void Start()
    {
        spawnerRiser = transform.GetComponent<CameraRiser>();
        cameraRiser = Camera.main.GetComponent<CameraRiser>();
        tetrominoFactory = GetComponent<TetrominoFactory>();
        if (tetrominoFactory == null)
        {
            Debug.LogError("TetrominoFactory component not found!");
        }
        
    }

    private void spawnTetramino() {
        string randomShape = shapes[Random.Range(0, shapes.Length)];
        GameObject tetromino = tetrominoFactory.CreateTetromino(randomShape);
        tetromino.transform.position = spawnPosition.position;
    }

    private void trySpawnTetramino() {
        if (!spawnedAndActive) {
            PauseMenuManager.Instance.ResumeGameNoMenu();
            spawnedAndActive = true;
            uint currentHeight = HeightCalculator.CalculateHeight(platformPosition.position);
            cameraRiser.UpdateTargetHeight(currentHeight);
            spawnerRiser.UpdateTargetHeight(currentHeight);
            TowerManager.Instance.UpdateMaxHeight(currentHeight);
            Debug.Log("CurrentHeight:"+currentHeight);
            spawnTetramino();
        }
    }

    void Update()
    {
        trySpawnTetramino();
    }
}
