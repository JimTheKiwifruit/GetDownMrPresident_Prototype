using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InspectionCam : MonoBehaviour {

	public float sensitivity = 0.4f;
	
	void Start() {
		
	}
	
	void Update() {
		Vector3 v = new Vector3(Input.GetAxis("HorizontalInspect"), 0, Input.GetAxis("VerticalInspect"));
		v = transform.TransformDirection(v);
		v = Vector3.ProjectOnPlane(v, Vector3.down);
		v *= sensitivity;
		transform.Translate(v, Space.World);
	}
	
}
