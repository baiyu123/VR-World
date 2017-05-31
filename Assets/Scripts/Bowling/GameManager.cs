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
	//public GameObject[] Displays;
	public TMPro.TextMeshPro[] scoreDisplay;
	public GameObject[] blockers;

	private List<StoreTransform> bowlingBallsTransform;
	private List<StoreTransform> kegalsTransform;

	private Animator[] blockerAnim;

	private int[] hitCount;
	private int[] numOfStandingKegels;
	private int[] Score;
	private const int numOfLane = 3;

	void Start () {
		bowlingBallsTransform = new List<StoreTransform> ();
		kegalsTransform = new List<StoreTransform> ();

		foreach (GameObject ball in bowlingBalls) {
			bowlingBallsTransform.Add (new StoreTransform(ball.transform));
		}

		foreach (GameObject kegel in kegels) {
			kegalsTransform.Add (new StoreTransform (kegel.transform));
		}

		hitCount = new int[numOfLane]{ 0, 0, 0 };
		numOfStandingKegels = new int[numOfLane]{10,10,10};
		Score = new int[numOfLane]{ 0, 0, 0 };

		blockerAnim = new Animator[blockers.Length];
		for (int i = 0; i < blockers.Length; i++) {
			blockerAnim[i] = blockers[i].GetComponent<Animator> ();
		}


	}

	void Update(){
		
	}

	public void Reset(){
		ResetBalls ();
		ResetAllKegels ();

		hitCount = new int[numOfLane]{ 0, 0, 0 };
		numOfStandingKegels = new int[numOfLane]{10,10,10};
		Score = new int[numOfLane]{ 0, 0, 0 };
		UpdateScore ();
	}

	public void ResetLane(int laneNum){
		ResetOneLaneKegels (laneNum);
		ResetBalls ();
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

	public void ResetAllKegels(){
		for (int i = 0; i < kegels.Length; i++) {
				kegels [i].transform.position = kegalsTransform [i].position;
				kegels [i].transform.rotation = kegalsTransform [i].rotation;
		}
	}

	private void ResetOneLaneKegels(int laneNum){
		for (int i = laneNum * 10; i < laneNum * 10 + 10; i++) {
			kegels [i].transform.position = kegalsTransform [i].position;
			kegels [i].transform.rotation = kegalsTransform [i].rotation;
		}
	}

	//on each ball thrown, calculate score and update score in each lane
	public void BallThrown(int laneNum){
		hitCount[laneNum]++;
		Invoke ("calculateAndUpdateScore",2);
		if (hitCount[laneNum] >= 2) {
			//Invoke ("EndRound", 2);
			StartCoroutine(EndRoundWithLaneNum(laneNum,2f));
			hitCount[laneNum] = 0;
		}
	}

	private void calculateAndUpdateScore(){
		CalculateScore ();
		UpdateScore ();
		for(int i = 0; i < numOfLane; i++){
			if (CheckStrikeByScore (i)) {
				EndRoundWithLaneNum(i);
			}
		}
	}

	private bool CheckStrikeByScore(int laneNum){
		if (Score [laneNum] == 10)
			return true;
		return false;
	}

	private void CalculateScore(){
		//k is the lane number, calculate score for each lane
		for (int k = 0; k < numOfStandingKegels.Length; k++) {
			numOfStandingKegels[k] = 0;
			for (int i = 0; i < 10; i++) {
				if (Vector3.Dot (kegels [k*10+i].transform.up, Vector3.up) > 0.8) {
					numOfStandingKegels[k]++;
				}
			}
			Score[k] = 10 - numOfStandingKegels[k];
		}
	}

	private void UpdateScore(){
		for (int i = 0; i < scoreDisplay.Length; i++) {
			scoreDisplay[i].SetText ("Score: "+Score[i].ToString());
		}

	}

	IEnumerator EndRoundWithLaneNum(int laneNum,float delayTime){
		yield return new WaitForSeconds (delayTime);
		blockerAnim[laneNum].SetTrigger ("RoundEnd");
	}

	private void EndRoundWithLaneNum(int laneNum){
		blockerAnim[laneNum].SetTrigger ("RoundEnd");
	}

}
