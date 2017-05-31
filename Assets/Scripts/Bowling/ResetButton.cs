using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	public GameManager manager;

	void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Controller")) {
			manager.Reset ();
		}
	}
}
