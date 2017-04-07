/* 
 * Originally from the asset Head Look Controller
 * Modified by Zhaorui in 2017
 * */
using UnityEngine;
using System.Collections;

public class CursorHit : MonoBehaviour {
	
	public HeadLookController headLook;//the corgi dog model
	private float offset = 1.5f;
	
	// Update is called once per frame
	void LateUpdate () {
		headLook.target = transform.position;
	}
}
