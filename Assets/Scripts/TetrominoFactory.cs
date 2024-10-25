using UnityEngine;

public class TetrominoFactory : MonoBehaviour
{
    public GameObject blockPrefab;
    public float tetrominoMass = 4.0f;
    public uint size = 2;

    public GameObject CreateTetromino(string shapeType)
    {
        GameObject tetromino = new GameObject(shapeType);

        

        Vector2[] shapePositions = GetShapePositions(shapeType);

        Color randomColor = new Color(Random.value, Random.value, Random.value);

        foreach (var pos in shapePositions)
        {
            GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity, tetromino.transform);
            block.transform.localScale = new Vector3(size, size, size);
            block.tag = "IsStationare";

            
            Renderer blockRenderer = block.GetComponent<Renderer>();
            if (blockRenderer != null)
            {
                blockRenderer.material.color = randomColor;
            }
        }

        Rigidbody rb = tetromino.AddComponent<Rigidbody>();
        rb.mass = tetrominoMass;

        var colider = tetromino.AddComponent<BoxCollider>();
        colider.isTrigger = true;

        tetromino.AddComponent<Movement_v2>();
        tetromino.tag = "Tetranomino";
        return tetromino;
    }

    private Vector2[] GetShapePositions(string shapeType)
    {
        switch (shapeType)
        {
            case "I":
                return new Vector2[] {
                    new Vector2(-size, 0), new Vector2(0, 0), new Vector2(size, 0), new Vector2(2*size, 0)
                };
            case "O":
                return new Vector2[] {
                    new Vector2(0, 0), new Vector2(size, 0), new Vector2(0, size), new Vector2(size, size)
                };
            case "T":
                return new Vector2[] {
                    new Vector2(-size, 0), new Vector2(0, 0), new Vector2(size, 0), new Vector2(0, size)
                };
            case "L":
                return new Vector2[] {
                    new Vector2(-size, 0), new Vector2(0, 0), new Vector2(size, 0), new Vector2(size, size)
                };
            case "J":
                return new Vector2[] {
                    new Vector2(-size, size), new Vector2(-size, 0), new Vector2(0, 0), new Vector2(size, 0)
                };
            case "S":
                return new Vector2[] {
                    new Vector2(0, 0), new Vector2(size, 0), new Vector2(-size, size), new Vector2(0, size)
                };
            case "Z":
                return new Vector2[] {
                    new Vector2(-size, 0), new Vector2(0, 0), new Vector2(0, size), new Vector2(size, size)
                };
            default:
                Debug.LogError("Unknown shape type: " + shapeType);
                return new Vector2[0];
        }
    }
}
