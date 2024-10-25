using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_v2 : MonoBehaviour
{
    private Transform transf;

    [SerializeField]
    private float baseSpeed = 2f;
    [SerializeField]
    private float acceleration = 8f;

    private float speed;

    [SerializeField]
    private float sideSpeed = 1f;
    
    private bool isStationare = false;
    private Movement_v2 movement_script;
    private Rigidbody rigid = null;
    private bool ignoreCollisions = false;

    void Start()
    {
        transf = transform;
        speed = baseSpeed;

        movement_script = transf.GetComponent<Movement_v2>();
        rigid = transf.GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    
    void Update()
    {
        if (!isStationare) {
            CheckForSpeedUp();
            moveThis();
        }
        else {
            movement_script.enabled = false;
        }
    }

    private void OnDestroy()
    {
        CheckIfStillActive();
    }

    private void CheckIfStillActive()
    {
        if (!isStationare) {
            Spawner.spawnedAndActive = false;
        }

        Debug.Log("checkActive called as Tetromino is being destroyed.");
    }

    void moveThis() {
        transf.transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime, Space.World);
        //Debug.Log(speed);

        Vector3 moveDir = new Vector3(0, 0, 0);
        moveDir += Input.GetKeyDown(KeyCode.W) ? new Vector3(0f, 0f, sideSpeed) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.S) ? new Vector3(0f, 0f, -sideSpeed) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.D) ? new Vector3(sideSpeed, 0f, 0f) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.A) ? new Vector3(-sideSpeed, 0f, 0f) : Vector3.zero;
        
        transf.transform.position += moveDir;

        Vector3 rotDir = new Vector3(0, 0, 0);
        rotDir += Input.GetKeyDown(KeyCode.Q) ? new Vector3(90f, 0f, 0f) : Vector3.zero;
        rotDir += Input.GetKeyDown(KeyCode.E) ? new Vector3(0f, 90f, 0f) : Vector3.zero;
        transf.transform.rotation *= Quaternion.Euler(rotDir);

    }

    private void CheckForSpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed *= acceleration;
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            speed = baseSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (ignoreCollisions) {
            return;
        }
        Debug.Log(""+transf.name);

        if (collision.collider.tag == "IsStationare") {
            isStationare = true;
            rigid.useGravity = true;
            Spawner.spawnedAndActive = false;
            ignoreCollisions = true;
        }
    }

}