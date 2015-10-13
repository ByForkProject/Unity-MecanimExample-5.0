using UnityEngine;
using System.Collections;

public class PlayerGenerator : MonoBehaviour {
	public GameObject teddy;
	public GameObject dude;
	public int showCount = 0;
	public int maxPlayerCount = 50;
	static int count = 0;
	static float lastTime = 0f;
	static float timeSpan = .1f;
	// Use this for initialization
	void Start () {
		lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (count < maxPlayerCount) {
			bool fired = Input.GetButton ("Fire1");

			if ((Time.time - lastTime)> timeSpan) {
				if (fired) {
					Instantiate (teddy, Vector3.zero, Quaternion.identity);
				}else{
					Instantiate (dude, Vector3.zero, Quaternion.identity);
				}
				lastTime = Time.time;
				count++;
				showCount = count;
			}
		}
	}
}
