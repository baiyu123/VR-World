using UnityEngine;
using System.Collections;

public class FloorColider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("Objects"), LayerMask.NameToLayer ("Floor"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
