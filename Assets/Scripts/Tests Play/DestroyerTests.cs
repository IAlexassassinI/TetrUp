using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class DestroyerTests : MonoBehaviour
{
    private GameObject destroyerObject;
    private GameObject tetrominoObject;
    private Destroyer destroyer;

    [SetUp]
    public void SetUp()
    {
        destroyerObject = new GameObject();
        destroyer = destroyerObject.AddComponent<Destroyer>();

        tetrominoObject = new GameObject("Tetromino");
        tetrominoObject.tag = "Tetranomino";
        tetrominoObject.AddComponent<BoxCollider>();
        Rigidbody rb = tetrominoObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(destroyerObject);
        Object.Destroy(tetrominoObject);
    }

    [UnityTest]
    public IEnumerator Destroyer_OnTriggerEnter_DestroysTetromino()
    {
        tetrominoObject.transform.position = destroyerObject.transform.position + new Vector3(0, 0, 1);

        Collider collider = tetrominoObject.GetComponent<Collider>();
        destroyer.OnTriggerEnter(collider);

        yield return null;

        Debug.Log(tetrominoObject);
        Assert.True(tetrominoObject == null);
    }

    [UnityTest]
    public IEnumerator Destroyer_OnTriggerEnter_DoesNotDestroyNonTetromino()
    {
        GameObject nonTetrominoObject = new GameObject("NonTetromino");
        nonTetrominoObject.AddComponent<BoxCollider>();
        Rigidbody rb = nonTetrominoObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        nonTetrominoObject.transform.position = destroyerObject.transform.position + new Vector3(0, 0, 1);

        Collider collider = nonTetrominoObject.GetComponent<Collider>();
        destroyer.OnTriggerEnter(collider);

        yield return null;

        Assert.IsNotNull(nonTetrominoObject);

        Object.Destroy(nonTetrominoObject);
    }
}
