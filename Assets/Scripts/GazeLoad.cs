using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeLoad : MonoBehaviour
{
    private bool isCounting = false;
    private float timer;
    public float duration = 2f;
    private bool gaze;
    public Image gazeimg;
    private GameObject ball;
    private GameObject XRRig;
    private GameObject trainCamPoint;
    private GameObject TrainFollowerEmpty;

    private GameObject rutscheLCamPoint;
    private GameObject rutscheMCamPoint;
    private GameObject rutscheRCamPoint;
    private GameObject RutscheLFollowEmpty;
    private GameObject RutscheMFollowEmpty;
    private GameObject RutscheRFollowEmpty;

    private bool followPath = false;
    private string activePath = "";
    private GameObject gazedAt;
    private string playerLocation = "start";

    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {

        XRRig = GameObject.Find("XR Rig");
        ball = GameObject.Find("Ball");
        TrainFollowerEmpty = GameObject.Find("TrainFollowerEmpty");
        trainCamPoint = GameObject.Find("TrainCamPoint");

        RutscheLFollowEmpty = GameObject.Find("RutscheLFollowEmpty");
        RutscheMFollowEmpty = GameObject.Find("RutscheMFollowEmpty");
        RutscheRFollowEmpty = GameObject.Find("RutscheRFollowEmpty");
        rutscheLCamPoint = GameObject.Find("RutscheLCamPoint");
        rutscheMCamPoint = GameObject.Find("RutscheMCamPoint");
        rutscheRCamPoint = GameObject.Find("RutscheRCamPoint");

        audioManager = FindObjectOfType<AudioManager>();

        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (gaze)
        {

            if (duration > timer)
            {
                timer += Time.deltaTime;
                gazeimg.fillAmount = timer / duration;
            }
            else
            {
                timer = 0f;
                gaze = false;
                gazeimg.fillAmount = 0;
                On();
            }
        }

        if (followPath)
        {
            switch (activePath)
            {
                case "train":
                    XRRig.transform.position = trainCamPoint.transform.position;
                    break;
                case "rutscheL":
                    XRRig.transform.position = rutscheLCamPoint.transform.position;
                    break;
                case "rutscheM":
                    XRRig.transform.position = rutscheMCamPoint.transform.position;
                    break;
                case "rutscheR":
                    XRRig.transform.position = rutscheRCamPoint.transform.position;
                    break;
                default:
                    break;
            }
        }

        switch (playerLocation)
        {
            case "start":
                GameObject.Find("StartPointBeacon").GetComponent<Renderer>().enabled = false;
                break;
            case "train":
                
                GameObject.Find("lowPolyTrainBeacon").GetComponent<Renderer>().enabled = false;
                break;
            case "rutsche":
                GameObject.Find("RutscheBeacon").GetComponent<Renderer>().enabled = false;
                GameObject.Find("RutscheRechts").GetComponent<Renderer>().enabled = true;
                GameObject.Find("RutscheMitte").GetComponent<Renderer>().enabled = true;
                GameObject.Find("RutscheLinks").GetComponent<Renderer>().enabled = true;
                break;
            default:
                break;
        }
    }
    private void On()
    {
        switch (gazedAt.name)
        { // trigger stuff here
            case "Ball":
                ball.GetComponent<BallController>().GrabBall();
                break;

            case "lowPolyTrainBeacon":
                reset();
                audioManager.Play("TrainWistle");
                playerLocation = "train";
                activePath = "train";
                TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 1;
                followPath = true;
                

                break;
            case "RutscheRechts":
                reset();
                audioManager.Play("SlideSound");
                activePath = "rutscheR";
                RutscheRFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 3;
                RutscheRFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0;
                followPath = true;
                break;
            case "RutscheMitte":
                reset();
                audioManager.Play("SlideSound");
                activePath = "rutscheM";
                RutscheMFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 3;
                RutscheMFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0;
                followPath = true;
                break;
            case "RutscheLinks":
                reset();
                audioManager.Play("SlideSound");
                activePath = "rutscheL";
                RutscheLFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 3;
                RutscheLFollowEmpty.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0;
                followPath = true;
                break;
            case "TrainSpeedUp":
                TrainSpeedUp();
                break;
            case "TrainStop":
                TrainStop();
                break;
            case "TrainSpeedDown":
                TrainSpeedDown();
                break;
            case "StartPointBeacon":
                reset();
                playerLocation = "start";
                teleToStart();
                break;
            case "RutscheBeacon":
                reset();
                playerLocation = "rutsche";
                XRRig.transform.position = GameObject.Find("RutscheCamPoint").transform.position;
                break;

            default:

                break;
        }

    }
    private void Off()
    {
        //untrigger stuff here
        if (gaze)
        {
            gaze = false;
            gazeimg.fillAmount = 0;
        }
    }
    private void StartCount()
    {
        timer = 0;
        gaze = true;
    }

    public void startGazeInteraction(GameObject obj)
    {
        gazedAt = obj;
        StartCount();
    }
    public void endGazeInteraction()
    {
        Off();
    }

    private void trainDrive()
    {
        //XRRig.transform.position = trainCamPoint.transform.position;
        XRRig.transform.parent = trainCamPoint.transform;
    }

    private void TrainStop() {
        TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 0;
        Debug.Log(TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed);
    }

    private void TrainSpeedUp()
    {
        TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed += 1;
        Debug.Log(TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed);
    }
    private void TrainSpeedDown()
    {
        TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed -= 1;
        Debug.Log(TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed);
    }
    private void reset()
    {
        GameObject.Find("StartPointBeacon").GetComponent<Renderer>().enabled = true;
        GameObject.Find("lowPolyTrainBeacon").GetComponent<Renderer>().enabled = true;
        GameObject.Find("RutscheBeacon").GetComponent<Renderer>().enabled = true;
        GameObject.Find("RutscheRechts").GetComponent<Renderer>().enabled = false;
        GameObject.Find("RutscheMitte").GetComponent<Renderer>().enabled = false;
        GameObject.Find("RutscheLinks").GetComponent<Renderer>().enabled = false;
        playerLocation = "";
        followPath = false;
        activePath = "";
        TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().speed = 0;
        TrainFollowerEmpty.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0;
        ball.GetComponent<Rigidbody>().velocity=Vector3.zero;
        ball.transform.position = Vector3.zero;
        Off();
    }
    private void teleToStart()
    {
        XRRig.transform.position = GameObject.Find("StartPoint").transform.position;
    }
}
