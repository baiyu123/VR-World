using UnityEngine;
using System.Collections;

public class ControllerGrab : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject;
	private GameObject grabbedObject;

	private SteamVR_Controller.Device Controller
	{
		get{ return SteamVR_Controller.Input ((int) trackedObj.index);}
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	private void SetCollidingObject(Collider col){
		if (!collidingObject && col.GetComponent<Rigidbody> ()) {
			collidingObject = col.gameObject;
		}
	}

	private FixedJoint AddFixedJoint(){
		FixedJoint joint = gameObject.AddComponent<FixedJoint> ();
		joint.breakForce = 20000;
		joint.breakTorque = 20000;
		return joint;
	}





	public void ReleaseObject(){
		FixedJoint joint = GetComponent<FixedJoint> ();
		if (joint) {
			joint.connectedBody = null;
			Destroy (joint);
		}
		if (grabbedObject) {
			grabbedObject.GetComponent<Rigidbody> ().velocity = Controller.velocity;
			grabbedObject.GetComponent<Rigidbody> ().angularVelocity = Controller.angularVelocity;
		}
	}

	public void GrabObject(){
		if (!collidingObject)
			return;
		grabbedObject = collidingObject;
		collidingObject = null;
		FixedJoint joint = AddFixedJoint();
		joint.connectedBody = grabbedObject.GetComponent<Rigidbody> ();
	}

	public void OnTriggerEnter(Collider other){
		SetCollidingObject (other);
	}

	public void OnTriggerStay(Collider other){
		SetCollidingObject (other);
	}

	public void OnTriggerExit(Collider other){
		collidingObject = null;
	}
}
