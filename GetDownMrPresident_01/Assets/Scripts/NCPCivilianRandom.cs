using UnityEngine;
using System.Collections;

public class NCPCivilianRandom : MonoBehaviour {

	NavMeshAgent agent;
	Animator animator;
	bool inCrowd;

	Vector3 civPosition;
	int idleCounter = 0;
	int counter = 0;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		civPosition = agent.transform.position;

		StartCoroutine(Run());
	}

	void Update()
	{
		animator.SetFloat("Speed", agent.velocity.magnitude);
		counter++;


		if (Mathf.Abs (civPosition.magnitude - agent.transform.position.magnitude) < 2) {
			idleCounter++;

		} 

		if (idleCounter > 180) {
			StopCoroutine (Run ());
			StartCoroutine (Run ());
			idleCounter = 0;
		}


		if (counter > 100)
		{
			civPosition = agent.transform.position;
			counter = 0;

		}


	}


	public void MoveRandom()
	{
		//        if (transform.position.x < 0)
		//        {
		//            
		//            if (transform.position.z < 0)
		//            {
		//                agent.SetDestination(new Vector3(Random.Range(-12f, 3f), 0, Random.Range(-12f, 12f)));
		//            }
		//            else
		//            {
		//                agent.SetDestination(new Vector3(Random.Range(-12f, 12f), 0, Random.Range(3f, 11f)));
		//            }
		//           
		//        }
		//        else if (transform.position.z < 0)
		//        {
		//            agent.SetDestination(new Vector3(Random.Range(-12f, 12f), 0, Random.Range(-12f, -5f)));
		//        }
		//        else
		//        {
		//
		//            agent.SetDestination(new Vector3(Random.Range(-12f, -5f), 0, Random.Range(-12f, -5f)));
		//        }

		if (transform.position.x > 0) {
			if (transform.position.z > 0) {

				agent.SetDestination(new Vector3(Random.Range(0f, 13f), 0, Random.Range(-13f, 0f)));
			} else if (transform.position.z < 0) {

				agent.SetDestination(new Vector3(Random.Range(-13f, 0f), 0, Random.Range(-13f, 0f)));
			}
		} else if (transform.position.x < 0) {
			if (transform.position.z > 0) {

				agent.SetDestination(new Vector3(Random.Range(0f,13f), 0, Random.Range(0f,13f)));
			} else if (transform.position.z < 0) {

				agent.SetDestination(new Vector3(Random.Range(-13f, 0f), 0, Random.Range(0f,13f)));
			}

		}

	}

	IEnumerator Run()
	{
		while (true)
		{
			MoveRandom();

			while (agent.velocity.magnitude > 0)
				yield return null;
			yield return new WaitForSeconds(1);

		}
	}

}
