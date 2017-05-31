using UnityEngine;
using System.Collections;

public class ResetKegels : MonoBehaviour {
	public GameManager manager;
	public int laneNum;
	public void Reset(){
		manager.ResetLane (laneNum);
	}

}
