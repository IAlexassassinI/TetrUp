using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using System.Collections;
public class TetrominoFactoryTests : InputTestFixture
{
    private GameObject testObject;
    private TetrominoFactory tetrominoFactory;
    private GameObject blockPrefab;

    [SetUp]
    public void SetUp()
    {

        testObject = new GameObject();
        tetrominoFactory = testObject.AddComponent<TetrominoFactory>();

        blockPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tetrominoFactory.blockPrefab = blockPrefab;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testObject);
        Object.Destroy(blockPrefab);
    }

    [Test]
    public void CreateTetromino_CreatesTetrominoOfTypeI()
    {
        GameObject tetromino = tetrominoFactory.CreateTetromino("I");

        Assert.IsNotNull(tetromino);
        Assert.AreEqual("I", tetromino.name);
        Assert.AreEqual(4, tetromino.transform.childCount);

        Rigidbody rb = tetromino.GetComponent<Rigidbody>();
        Assert.IsNotNull(rb);
        Assert.AreEqual(tetrominoFactory.tetrominoMass, rb.mass);

        BoxCollider collider = tetromino.GetComponent<BoxCollider>();
        Assert.IsNotNull(collider);
        Assert.IsTrue(collider.isTrigger);


        Vector2[] expectedPositions = new Vector2[] {
            new Vector2(-tetrominoFactory.size, 0),
            new Vector2(0, 0),
            new Vector2(tetrominoFactory.size, 0),
            new Vector2(2 * tetrominoFactory.size, 0)
        };

        for (int i = 0; i < tetromino.transform.childCount; i++)
        {
            GameObject block = tetromino.transform.GetChild(i).gameObject;
            Vector2 expectedPosition = expectedPositions[i];
            Assert.AreEqual(new Vector3(expectedPosition.x, expectedPosition.y, 0), block.transform.position);
            Assert.AreEqual("IsStationare", block.tag);
        }
    }

    [Test]
    public void CreateTetromino_CreatesTetrominoOfTypeO()
    {
        GameObject tetromino = tetrominoFactory.CreateTetromino("O");

        Assert.IsNotNull(tetromino);
        Assert.AreEqual("O", tetromino.name);
        Assert.AreEqual(4, tetromino.transform.childCount);

        Rigidbody rb = tetromino.GetComponent<Rigidbody>();
        Assert.IsNotNull(rb);
        Assert.AreEqual(tetrominoFactory.tetrominoMass, rb.mass);

        BoxCollider collider = tetromino.GetComponent<BoxCollider>();
        Assert.IsNotNull(collider);
        Assert.IsTrue(collider.isTrigger);


        Vector2[] expectedPositions = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(tetrominoFactory.size, 0),
            new Vector2(0, tetrominoFactory.size),
            new Vector2(tetrominoFactory.size, tetrominoFactory.size)
        };

        for (int i = 0; i < tetromino.transform.childCount; i++)
        {
            GameObject block = tetromino.transform.GetChild(i).gameObject;
            Vector2 expectedPosition = expectedPositions[i];
            Assert.AreEqual(new Vector3(expectedPosition.x, expectedPosition.y, 0), block.transform.position);
            Assert.AreEqual("IsStationare", block.tag);
        }
    }

    [UnityTest]
    public IEnumerator CreateTetromino_ColorsBlocksRandomly()
    {
        GameObject tetromino = tetrominoFactory.CreateTetromino("T");

        Assert.IsNotNull(tetromino);

        for (int i = 0; i < tetromino.transform.childCount; i++)
        {
            GameObject block = tetromino.transform.GetChild(i).gameObject;
            Renderer blockRenderer = block.GetComponent<Renderer>();
            Assert.IsNotNull(blockRenderer);
            Assert.AreNotEqual(Color.clear, blockRenderer.material.color);
        }

        yield return null;
    }

    [Test]
    public void CreateTetromino_ReturnsNullForUnknownShape()
    {
        GameObject tetromino = tetrominoFactory.CreateTetromino("UnknownShape");

        Assert.IsNotNull(tetromino);
        Assert.AreEqual(0, tetromino.transform.childCount);
    }
}
