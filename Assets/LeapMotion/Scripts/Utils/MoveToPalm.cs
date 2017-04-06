using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class MoveToPalm : MonoBehaviour
{

    public GameObject followTarget;
    Controller controller;
    private HandController hc;
    // Use this for initialization
    void Start()
    {
        controller = new Controller();
        //controller = GameObject.Find("HandController");
        if (hc == null)
            hc = GameObject.FindWithTag("HandController").GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.IsConnected)
        { //controller is a Controller object
            Frame frame = controller.Frame(); //The latest frame
            foreach (Hand hand in frame.Hands)
            {
                if (hand.IsRight)
                {

                    // For Orion, can use leapService and LeapProvider class for quicker operations.
                    //transform.position = hand.PalmPosition.ToVector3() +
                    //hand.PalmNormal.ToVector3() *
                    //(transform.localScale.y * .5f + .02f);
                    //transform.rotation = hand.Basis.Rotation();

                    Leap.Vector position = hand.PalmPosition;
                    Vector3 unityPosition = position.ToUnityScaled(false);
                    // print(unityPosition);
                    Vector3 worldPosition = hc.transform.TransformPoint(unityPosition);
                    //print(worldPosition);
                    Leap.Vector norm = hand.PalmNormal;
                    Vector3 unityNorm = norm.ToUnityScaled(false);
                    followTarget.transform.position = worldPosition + unityNorm * (followTarget.transform.localScale.y * .5f + .02f);
                    followTarget.transform.rotation = hand.Basis.Rotation();
                }
            }
        }
    }
}
