using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {
	public float AvatarRange = 25;

	private Animator anim;
	private float SpeedDampTime = .25f;
	private float DirectionDampTime = .25f;
	private Vector3 TargetPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		int r = Random.Range (0, 50);

		anim.SetBool ("Jump", r == 20);
		anim.SetBool ("Dive", r == 30);

		if (Vector3.Distance (TargetPosition, anim.rootPosition) > 5) {
			//Speed
			anim.SetFloat ("Speed", 1, SpeedDampTime, Time.deltaTime);

			//Direction
			Vector3 currentDirection = anim.rootRotation * Vector3.forward;
			Vector3 wantedDirection = (TargetPosition - anim.rootPosition).normalized;

			if (Vector3.Dot (currentDirection, wantedDirection) >  0) { // < 90
				anim.SetFloat ("Direction", 
				               Vector3.Cross (currentDirection, wantedDirection).y, 
				               DirectionDampTime,
				               Time.deltaTime);
			}else{ // > 90
				anim.SetFloat ("Direction",
				               Vector3.Cross (currentDirection, wantedDirection).y>0?1f:-1f, // left hand rules, > 0 turn right, < 0 turn left
				               DirectionDampTime,
				               Time.deltaTime);
			}
		}else{
			anim.SetFloat ("Speed", 0, SpeedDampTime, Time.deltaTime);

			if (anim.GetFloat ("Speed") < 0.01f) {
				TargetPosition = new Vector3(
					Random.Range (-AvatarRange, AvatarRange),
					0,
					Random.Range (-AvatarRange, AvatarRange)
					);
			}
		}

		var nextState = anim.GetNextAnimatorStateInfo(0);

		Debug.Log (nextState.tagHash);
		if (nextState.IsName ("Base Layer.Dying")) {
			anim.SetBool ("Dying", false);

		}
	}

	void OnCollisionEnter(Collision collision){
		AnimatorStateInfo currentStateInfo = anim.GetCurrentAnimatorStateInfo (0);
		AnimatorStateInfo nextStateInfo = anim.GetNextAnimatorStateInfo (0);

		string dyingStateName = "BaseLayer.Dying";
		if (!currentStateInfo.IsName (dyingStateName) && !nextStateInfo.IsName (dyingStateName)) {
			anim.SetBool ("Dying", true);
		}
	}
}
