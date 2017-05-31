using UnityEngine;
using System.Collections;

public class BallHitDetect : MonoBehaviour {
	public GameManager manager;

	void OnTriggerEnter(Collider collider){
		if (collider.CompareTag ("Ball")) {
			manager.BallThrown ();
		}
	}
}
