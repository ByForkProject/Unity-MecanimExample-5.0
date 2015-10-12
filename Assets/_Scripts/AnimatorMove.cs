using UnityEngine;
using System.Collections;

public class AnimatorMove : MonoBehaviour {
	public float directionDampTime =.25f;

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (stateInfo.IsName ("Base Layer.Run")) {
			if (Input.GetButton("Fire1")) {
				anim.SetBool ("Jump", true);
			}
		}else{
			anim.SetBool ("Jump", false);
		}

		if (Input.GetButton ("Fire2")) {
			anim.SetBool ("Hi", !anim.GetBool("Hi"));
		}

		float speed = Mathf.Sqrt (h*h + v*v);
		anim.SetFloat ("Speed", speed);
		anim.SetFloat ("Direction", h, directionDampTime, Time.deltaTime);
	}
}
