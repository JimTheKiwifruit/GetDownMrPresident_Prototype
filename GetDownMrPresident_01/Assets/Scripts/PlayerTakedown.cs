using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerTakedown : MonoBehaviour {
    
    GameScore gameScore;

    public float minDistance = 1.5f;
	public int playerNum = 1;

    void Start()
    {
        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
        playerNum = gameScore.getPlayerNum(this.name);
    }
    
	void Update() {
		if (playerNum > -1 && Input.GetButtonDown("AButton" + playerNum)) {

            print("Button 1");
            print(Input.GetButtonDown("AButton1"));
            print("Button 2");
            print(Input.GetButtonDown("AButton2"));
            //print(this.name);
            //print(playerNum);

            Takedown();
		}
	}
	
	public bool TargetInRange(PlayerTakedown target){
		return Vector3.Distance(target.transform.position, this.transform.position) < minDistance;
	}

	public void DoTakedown(PlayerTakedown target){
		target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		target.gameObject.GetComponent<Rigidbody>().useGravity = true;
		target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		target.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 80));
		//Destroy(target.gameObject.GetComponent<PlayerTakedown>());
		//Destroy(target.gameObject.GetComponent<PlayerMovement>());
		//Destroy(target.gameObject.GetComponent<NPCPresidentAI>());
		//Destroy(target.gameObject.GetComponent<NavMeshAgent>());
		//gameObject.tag = "Untagged";
		GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.transform.position - transform.position) * 35, ForceMode.VelocityChange);
	}

	
	public void Takedown() {
		string name = this.name;
		PlayerTakedown[] targets = FindObjectsOfType<PlayerTakedown>();
		foreach (PlayerTakedown target in targets) {
			if (target.playerNum == this.playerNum)
				continue;
			if( name.Equals("Dev_Player_01 (Assassin)") && target.name.Equals("President") && TargetInRange(target)) {
                print("president takedown");
				RoundManager.main.PresidentDown();
				DoTakedown(target);
			} else if ( name.Equals("Dev_Player_01 (Bodyguard)") && target.name.Equals("Dev_Player_01 (Assassin)") && TargetInRange(target)) {
                print("assassin takedown");
				RoundManager.main.PresidentSaved();
				DoTakedown(target);
			}
		}
		
		
		
		
		//PlayerTakedown[] targets = FindObjectsOfType<PlayerTakedown>();
		//foreach (PlayerTakedown target in targets) {
		//	if (target.playerNum == this.playerNum)
		//		continue;
		//	if (Vector3.Distance(target.transform.position, this.transform.position) < minDistance) {
		//		print("takedonw!!!! " + target.playerNum + "---" + this.playerNum);
		//		print(Vector3.Distance(target.transform.position, transform.position));

		//		if (playerNum == 1 && this.name.Equals("Dev_Player_01 (Assassin)") {
		//			if (target.gameObject.CompareTag("President")) {
		//				RoundManager.main.PresidentDown();
		//			} else {
		//				break;
		//			}
		//		} else if (playerNum == 2 && this.name.Equals("Dev_Player_01 (Bodyguard)") {
		//			if (target.gameObject.CompareTag("Assassin")) {
		//				RoundManager.main.PresidentSaved();
		//			}
		//		} else {
		//			continue;
		//		}

				
		//	}
		//}
		
	}

}
