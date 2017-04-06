using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class gesture_animation : MonoBehaviour {
    public Animator anim;
    private int status;
    private int longtime;// to control the random movements if stays in long time
    private int prevStatus;
    private HandController hc;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        prevStatus = 0;
	}

    private bool IsHand(Collider other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsHand(other))
        {
            print("Yay! A hand collided!");
            if (status != 2) { 
                prevStatus = status; // keep the previous status
                print("Keep previous status "+prevStatus.ToString());
            }
            anim.SetInteger("status", 2);//change to lay
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsHand(other))
        {
            print("Hand removed");
            anim.SetInteger("status", 0); // change to previous status(sit/idle)
        }
    }

    // Update is called once per frame
    void Update () {
        status = anim.GetInteger("status");
        longtime = anim.GetInteger("longtime");

        switch (status)
        {
            case 0:
                print("common idle");
                break;

            case 1:
                print("sit idle");
                break;

            case 2:
                print("lay idle");
                break;
        }
	}
}
