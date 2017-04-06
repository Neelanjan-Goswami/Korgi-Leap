using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class gesture_animation : MonoBehaviour {
    public Animator anim;
    private int status;
    private int longtime;// to control the random movements if stays in long time
    private HandController hc;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
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
        }
        print("haha");
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
