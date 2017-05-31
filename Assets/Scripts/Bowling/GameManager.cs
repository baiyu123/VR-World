using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreTransform
{
	public Vector3 position;
	public Quaternion rotation;

	public StoreTransform(Transform transform){
		position = transform.position;
		rotation = transform.rotation;
	}
}

public class GameManager : MonoBehaviour {

	public GameObject[] bowlingBalls;
	public GameObject[] kegels;
	public TMPro.TextMeshPro scoreDisplay;
	public GameObject blocker;

	private List<StoreTransform> bowlingBallsTransform;
	private List<StoreTransform> kegalsTransform;

	private Animator blockerAnim;

	private int hitCount;
	private int numOfStandingKegels;
	private int Score;

	void Start () {
		bowlingBallsTransform = new List<StoreTransform> ();
		kegalsTransform = new List<StoreTransform> ();

		foreach (GameObject ball in bowlingBalls) {
			bowlingBallsTransform.Add (new StoreTransform(ball.transform));
		}

		foreach (GameObject kegel in kegels) {
			kegalsTransform.Add (new StoreTransform(kegel.transform));
		}
		blockerAnim = blocker.GetComponent<Animator> ();

		hitCount = 0;
	}

	void Update(){
		
	}

	public void Reset(){
		ResetBalls ();
		ResetKegels ();

		Score = 0;
		numOfStandingKegels = 10;
		hitCount = 0;
		UpdateScore ();
	}

	public void ResetBalls(){
		for (int i = 0; i < bowlingBalls.Length; i++) {
			bowlingBalls [i].transform.position = bowlingBallsTransform [i].position;
			bowlingBalls [i].transform.rotation = bowlingBallsTransform [i].rotation;
		}
		foreach (GameObject ball in bowlingBalls) {
			ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		}
	} 

	public void ResetKegels(){
		for (int i = 0; i < kegels.Length; i++) {
			kegels [i].transform.position = kegalsTransform [i].position;
			kegels [i].transform.rotation = kegalsTransform [i].rotation;
		}
	}

	public void BallThrown(){
		hitCount++;
		Invoke ("CalculateScore",2);
		Invoke ("UpdateScore", (float)2.1);
		if (hitCount >= 2) {
			Invoke ("EndRound", 2);
			hitCount = 0;
		}
	}

	private void CalculateScore(){
		numOfStandingKegels = 0;
		for (int i = 0; i < kegels.Length; i++) {
			if (Vector3.Dot (kegels [i].transform.up, Vector3.up) > 0.8) {
				numOfStandingKegels++;
			}
		}
		Score = 10 - numOfStandingKegels;
	}

	private void UpdateScore(){
		scoreDisplay.SetText ("Score: "+Score.ToString());
	}

	private void EndRound(){
		blockerAnim.SetTrigger ("RoundEnd");
	}

}
