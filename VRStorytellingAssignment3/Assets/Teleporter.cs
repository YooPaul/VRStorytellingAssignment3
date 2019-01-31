using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Teleporter : MonoBehaviour {

    public BezierCurve3PointRenderer bezierRenderer;
    private GameObject camera;
    public PostProcessingBehaviour postProcessor;

    private Vector3 height;
    private bool isMoving;
    private Vector3 destination;
    private Vector3 direction;
	// Use this for initialization
	void Start () {
        camera = GameObject.Find("OVRCameraRig");
        height = new Vector3(0, 0.5f, 0);
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && bezierRenderer.canTeleport && !isMoving) // Oculus Touch controller trigger press
        {
            isMoving = true;
            postProcessor.enabled = true;
            destination = bezierRenderer.endPoint + height;
            direction = destination - camera.transform.position;

            //Debug.Log(bezierRenderer.endPoint);
        }

        if(isMoving)
        {
            camera.transform.position += direction * Time.deltaTime * 4;
            if(Vector3.Distance(camera.transform.position , destination) < 0.05)
            {
                camera.transform.position = destination;
                isMoving = false;
                postProcessor.enabled = false;
            }
        }
        

	}
}
