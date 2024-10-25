using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class MovementTests : InputTestFixture
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
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(testObject);
    }

    [Test]
    public void InitialSpeedIsBaseSpeed()
    {
        Assert.AreEqual(movement.baseSpeed, movement.speed);
    }

    [UnityTest]
    public IEnumerator SpeedIncreasesWhenSpacePressed()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        Press(keyboard.spaceKey);
        
        yield return new WaitForSeconds(1f);
        movement.CheckForSpeedUp();
       
        movement.Invoke("Update", 0f);
        yield return null;


        Assert.AreNotEqual(movement.baseSpeed, movement.speed);
    }

    [Test]
    public void RotationChange()
    {
        var initialRotation = movement.transform.rotation;
        movement.transform.rotation *= Quaternion.Euler(new Vector3(90f, 0f, 0f));
        Assert.AreNotEqual(initialRotation, movement.transform.rotation);
    }

    [UnityTest]
    public IEnumerator PlayerCanMoveWithWASD()
    {
        var keyboard = InputSystem.AddDevice<Keyboard>();
        testObject.transform.position = Vector3.zero;

        
        Press(keyboard.wKey);
        yield return new WaitForSeconds(0.1f);
        movement.Invoke("Update", 0f);
        yield return null;

        
        Assert.Greater(testObject.transform.position.z, 0);

        
        testObject.transform.position = Vector3.zero;

        
        Press(keyboard.aKey);
        yield return new WaitForSeconds(0.1f);
        movement.Invoke("Update", 0f);
        yield return null;

        
        Assert.Less(testObject.transform.position.x, 0);

        
        testObject.transform.position = Vector3.zero;

        
        Press(keyboard.sKey);
        yield return new WaitForSeconds(0.1f);
        movement.Invoke("Update", 0f);
        yield return null;

       
        Assert.Less(testObject.transform.position.z, 0);

       
        testObject.transform.position = Vector3.zero;

        
        Press(keyboard.dKey);
        yield return new WaitForSeconds(0.1f);
        movement.Invoke("Update", 0f);
        yield return null;

        
        Assert.Greater(testObject.transform.position.x, 0);
    }
}
