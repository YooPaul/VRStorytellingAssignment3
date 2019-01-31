using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

        if(OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
        {
            Debug.Log("Rotate Left");
            this.transform.Rotate(Vector3.up, -45);
        }
        else if(OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
        {
            Debug.Log("Rotate Right");
            this.transform.Rotate(Vector3.up, 45);
        }
	}
}
