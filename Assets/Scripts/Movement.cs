using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform transf;
    private Transform parentTransf;
    [SerializeField]
    private float speed = 2f;
    private bool isStationare = false;
    private Movement movement_script;
    private Rigidbody rigid = null;

    void Start()
    {
        transf = transform;
        parentTransf = transform.parent;

        movement_script = transf.GetComponent<Movement>();
        rigid = transf.GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    
    void Update()
    {
        if (!isStationare) {
            moveThis();
        }
        else {
            movement_script.enabled = false;
        }
    }


    void moveThis() {
        parentTransf.transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime, Space.World);
        Vector3 moveDir = new Vector3(0, 0, 0);
        moveDir += Input.GetKeyDown(KeyCode.W) ? new Vector3(0f, 0f, 0.5f) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.S) ? new Vector3(0f, 0f, -0.5f) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.D) ? new Vector3(0.5f, 0f, 0f) : Vector3.zero;
        moveDir += Input.GetKeyDown(KeyCode.A) ? new Vector3(-0.5f, 0f, 0f) : Vector3.zero;
        parentTransf.transform.position += moveDir;
        // i can tewst if correctly rotate in tests
        Vector3 rotDir = new Vector3(0, 0, 0);
        rotDir += Input.GetKeyDown(KeyCode.Q) ? new Vector3(90f, 0f, 0f) : Vector3.zero;
        rotDir += Input.GetKeyDown(KeyCode.E) ? new Vector3(0f, 90f, 0f) : Vector3.zero;
        parentTransf.transform.rotation *= Quaternion.Euler(rotDir);

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "IsStationare") {
            isStationare = true;
            rigid.useGravity = true;
        }
    }

}