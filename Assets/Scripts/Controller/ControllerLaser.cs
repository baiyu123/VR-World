using UnityEngine;
using System.Collections;

public class ControllerLaser : MonoBehaviour {

	public float laserRange=50f;
	public GameObject cameraRig;
	public LayerMask groundLayer;
	public Vector3 moveToPos;

	private LineRenderer laser;
	private bool useLaser=false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller{
		get{return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Start(){
		laser = GetComponent<LineRenderer> ();
	}

	void Update(){
		UpdateLaser ();
	}

	private void UpdateLaser(){
		if (!useLaser)
			laser.enabled = false;
		else {
			laser.enabled = true;
			RaycastHit hit;
			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, laserRange)) {
				Vector3 hitpoint = hit.point;
				laser.SetPosition (0, hit.point);
				laser.SetPosition (1, trackedObj.transform.position);
				if (((1 << hit.transform.gameObject.layer) & groundLayer) != 0) {
					moveToPos = hitpoint;
				}
			} else {
				laser.SetPosition (0, laserRange*transform.forward + trackedObj.transform.position);
				laser.SetPosition (1, trackedObj.transform.position);
			}
		}
	}
		
	public void UseLaser(){
		useLaser = true;
	}

	public void StopLaser(){
		useLaser = false;
	}

	public void teleport(){
		cameraRig.gameObject.transform.position = moveToPos;
	}

	public bool isUsingLaser(){
		return useLaser;
	}
}
