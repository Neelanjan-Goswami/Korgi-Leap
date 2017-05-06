/*
 * Created by Zhaorui Chen, 2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class KorgiAnimation : MonoBehaviour {
    private Animator anim;
    private int status;
    private int longtime;// to control the random movements if stays in long time
    private int rand;
    private int prevStatus;
    private float startTime;
    private float currentTime;
    Controller controller;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        longtime = 0;
        rand = 0;
        prevStatus = 0;
        startTime = 0;
        controller = new Controller();
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
            //startTime = currentTime;
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
            anim.SetInteger("status", status);
            //startTime = currentTime;
        }
    }

    // Update is called once per frame
    void Update () {

        status = anim.GetInteger("status");
        //longtime = anim.GetInteger("longtime");
        //currentTime = Time.time;

        switch (status)
        {
            case 0:
                //print("stand idle");
                /*
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
                }*/
                break;

            case 1:
                //print("sit idle");
                break;

            case 2:
                //print("lay idle");
                break;
        }
	}
}
