using UnityEngine;
using System.Collections;

public class ControllerFunctions : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private ControllerGrab controllerGrab;
	private ControllerLaser controllerLaser;

	private SteamVR_Controller.Device Controller{
		get{return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controllerGrab = GetComponent<ControllerGrab> ();
		controllerLaser = GetComponent<ControllerLaser> ();
	}

	void Update () {
		if (Controller.GetHairTriggerDown ()) {
			if (controllerLaser.isUsingLaser ()) {
				controllerLaser.teleport ();
			} else {
				controllerGrab.GrabObject ();
			}
		}
		if (Controller.GetHairTriggerUp ()) {
			controllerGrab.ReleaseObject ();
		}
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			controllerLaser.UseLaser ();
		} else {
			controllerLaser.StopLaser ();
		}
	}
}
