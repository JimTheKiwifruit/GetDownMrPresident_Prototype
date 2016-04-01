using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerTakedown : MonoBehaviour {

	public float minDistance = 1.5f;

	void Update() {
		if (Input.GetButtonDown("Takedown")) {
			Takedown();
		}
	}

	public void Takedown() {
		GameObject[] assassins = GameObject.FindGameObjectsWithTag("Assassin");
		foreach (GameObject ass in assassins) {
			if (Vector3.Distance(ass.transform.position, transform.position) < minDistance) {
				//Destroy(ass);
				ass.GetComponent<Collider>().isTrigger = false;
				ass.AddComponent<Rigidbody>().mass = 0.25f;
				ass.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 80));
				Destroy(ass.GetComponent<NavMeshAgent>());
				Destroy(ass.GetComponent<NPC>());
				gameObject.tag = "Untagged";
				GetComponent<Rigidbody>().AddForce(Vector3.Normalize(ass.transform.position - transform.position) * 35, ForceMode.VelocityChange);
			}
		}
	}
	
}
