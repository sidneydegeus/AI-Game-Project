using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Vector3 cameraPosition;
    public float CameraMoveSpeed;

	// Use this for initialization
	void Start () {
        cameraPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        moveCamera();
        zoomCamera();
        rotateCamera();

        transform.position = cameraPosition;
    }

    void moveCamera() {
        if (Input.GetKey("w")) {
            cameraPosition.z += CameraMoveSpeed;
        }
        if (Input.GetKey("s")) {
            cameraPosition.z -= CameraMoveSpeed;
        }
        if (Input.GetKey("a")) {
            cameraPosition.x -= CameraMoveSpeed;
        }
        if (Input.GetKey("d")) {
            cameraPosition.x += CameraMoveSpeed;
        }
    }

    void zoomCamera() {
        if (Input.GetKey("x")) {
            if (cameraPosition.y >= 20.0f)
                cameraPosition.y -= CameraMoveSpeed;
        }
        if (Input.GetKey("z")) {
            if (cameraPosition.y <= 50.0f)
                cameraPosition.y += CameraMoveSpeed;
        }
    }

    //not working!!!!!
    void rotateCamera() {
        if (Input.GetKey("q")) {
            transform.Rotate(0, Input.GetAxis("Horizontal") * 0.03f, 0, Space.World);
        }
        if (Input.GetKey("e")) {
            transform.Rotate(0, Input.GetAxis("Horizontal") * -0.03f, 0, Space.World);
        }
    }
}
