using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerTakedown : MonoBehaviour {

	public float minDistance = 1.5f;
	public int playerNum = 1;

	void Update() {
		if (Input.GetButtonDown("Takedown" + playerNum)) {
			Takedown();
		}
	}

	public void Takedown() {
		GameObject[] assassins = GameObject.FindGameObjectsWithTag("Assassin");
		foreach (GameObject ass in assassins) {
			if (Vector3.Distance(ass.transform.position, transform.position) < minDistance) {
				//Destroy(ass);
				//ass.GetComponent<Collider>().isTrigger = false;
				ass.GetComponent<Rigidbody>().useGravity = true;
				ass.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				ass.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 80));
				Destroy(ass.GetComponent<PlayerTakedown>());
				Destroy(ass.GetComponent<PlayerMovement>());
				gameObject.tag = "Untagged";
				GetComponent<Rigidbody>().AddForce(Vector3.Normalize(ass.transform.position - transform.position) * 35, ForceMode.VelocityChange);

				NPCCivilian[] civils = FindObjectsOfType<NPCCivilian>();
				foreach (NPCCivilian npc in civils) {
					npc.RunAway(ass.transform.position);
				}
				break;
			}
		}
	}
	
}
