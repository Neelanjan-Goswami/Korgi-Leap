using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class HandTracker : MonoBehaviour {
    private HandModel hand;
    public GameObject followTarget;

    // Use this for initialization
    void Start ()
    {
        hand = transform.parent.GetComponent<HandModel>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hand == null)
        {
            hand = transform.parent.GetComponent<HandModel>();
            followTarget.transform.position = hand.palm.position;
        }else
        {
            followTarget.transform.position = hand.palm.position;
        }
    }
}
