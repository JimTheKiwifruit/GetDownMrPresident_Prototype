using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPCCivilian : NPC {

	NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		StartCoroutine(Run());
	}

	void MoveRandom() {
		agent.SetDestination(new Vector3(Random.Range(-13f, 13f), 0, Random.Range(-13f, 13f)));
	}

	IEnumerator Run() {
		while (true) {
			MoveRandom();
			while (agent.velocity.magnitude > 0)
				yield return null;
			yield return new WaitForSeconds(Random.Range(1f, 5f));
		}
	}
	
}
