using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class gesture_animation : MonoBehaviour {
    public Animator anim;
    private int status;
    private int longtime;// to control the random movements if stays in long time
    private int rand;
    private int prevStatus;
    private float startTime;
    private float currentTime;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        longtime = 0;
        rand = 0;
        prevStatus = 0;
        startTime = 0;
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
            startTime = currentTime;
            status = 2;
            anim.SetInteger("status", status);//change to lay
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsHand(other))
        {
            print("Hand removed");
            status = 0;
            anim.SetInteger("status", 0); // change to previous status(sit/idle)
            startTime = currentTime;
        }
    }

    // Update is called once per frame
    void Update () {
        status = anim.GetInteger("status");
        longtime = anim.GetInteger("longtime");
        currentTime = Time.time;

        switch (status)
        {
            case 0:
                print("common idle");
                if (currentTime - startTime > 7)
                {
                    //anim.Play("CorgiIdleLong");
                    longtime = 1;
                    rand = Random.Range(0, 2);
                    anim.SetInteger("longtime", longtime);
                    anim.SetInteger("rand", rand);
                    startTime = currentTime;
                }else
                {
                    if (longtime == 1)
                    {
                        longtime = 0;
                        rand = 0;
                        anim.SetInteger("longtime", longtime);
                        anim.SetInteger("rand", rand);
                    }
                }
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
