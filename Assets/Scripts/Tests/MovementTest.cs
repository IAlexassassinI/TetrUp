using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTests
{
    private GameObject testObject;
    private Movement_v2 movement;

    [SetUp]
    public void SetUp()
    {

        testObject = new GameObject();
        movement = testObject.AddComponent<Movement_v2>();

        Rigidbody rb = testObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        movement.Invoke("Start", 0f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testObject);
    }

    

    [Test]
    public void SpeedIncreasesWhenSpacePressed()
    {
        movement.CheckForSpeedUp();
        movement.speed *= movement.acceleration; 

        Assert.AreNotEqual(movement.baseSpeed, movement.speed);
    }

    [Test]
    public void RotationChangeOnKeyInput()
    {
        movement.Invoke("Start", 0f);
        var initialRotation = movement.transform.rotation;

        
        movement.transform.rotation *= Quaternion.Euler(new Vector3(90f, 0f, 0f));

        
        Assert.AreNotEqual(initialRotation, movement.transform.rotation);
    }


    [UnityTest]
    public IEnumerator TestWASDMovement()
    {
        // Store the initial position
        Vector3 initialPosition = testObject.transform.position;

        // Simulate pressing the 'W' key
        InputSimulator.SimulateKeyPress(KeyCode.W);
        movement.Invoke("Update", 0f);
        //movement.Update(); // Call Update to process movement

        // Check the position after pressing 'W'
        Assert.AreNotEqual(initialPosition, testObject.transform.position);
        Assert.Greater(testObject.transform.position.z, initialPosition.z);

        // Reset position
        testObject.transform.position = initialPosition;

        // Simulate pressing the 'A' key
        InputSimulator.SimulateKeyPress(KeyCode.A);
        //movement.Update(); // Call Update to process movement
        movement.Invoke("Update", 0f);
        // Check the position after pressing 'A'
        Assert.AreNotEqual(initialPosition, testObject.transform.position);
        Assert.Less(testObject.transform.position.x, initialPosition.x);

        // Reset position
        testObject.transform.position = initialPosition;

        // Simulate pressing the 'S' key
        InputSimulator.SimulateKeyPress(KeyCode.S);
        //movement.Update(); // Call Update to process movement
        movement.Invoke("Update", 0f);
        // Check the position after pressing 'S'
        Assert.AreNotEqual(initialPosition, testObject.transform.position);
        Assert.Less(testObject.transform.position.z, initialPosition.z);

        // Reset position
        testObject.transform.position = initialPosition;

        // Simulate pressing the 'D' key
        InputSimulator.SimulateKeyPress(KeyCode.D);
        //movement.Update(); // Call Update to process movement
        movement.Invoke("Update", 0f);
        // Check the position after pressing 'D'
        Assert.AreNotEqual(initialPosition, testObject.transform.position);
        Assert.Greater(testObject.transform.position.x, initialPosition.x);

        yield return null;
    }

}
public static class InputSimulator
{
    public static void SimulateKeyPress(KeyCode key)
    {
        InputSimulatorManager.Instance.SimulateKeyPress(key);
    }
}

public class InputSimulatorManager : MonoBehaviour
{
    private static InputSimulatorManager _instance;

    public static InputSimulatorManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("InputSimulatorManager");
                _instance = obj.AddComponent<InputSimulatorManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();

    private void Update()
    {
        foreach (var key in keyStates.Keys)
        {
            if (keyStates[key])
            {
                Input.simulateMouseWithTouches = true;
            }
        }
    }

    public void SimulateKeyPress(KeyCode key)
    {
        keyStates[key] = true;
    }

    public void ReleaseKeyPress(KeyCode key)
    {
        keyStates[key] = false;
    }
}
