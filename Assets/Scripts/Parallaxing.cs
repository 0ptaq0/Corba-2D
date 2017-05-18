using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds; //array - list of all back/foregrounds parallaxed
    private float[] parallaxScales; //proportion of camera`s movement to move the bacground by
    public float smoothing = 0.5f;         //how smooth the parallax is. Must be above 0.

    private Transform cam;      //reference to the main cameras transform
    private Vector3 previousCameraPosition; //the postion of camera in previous frame

    //Is called before start(). Mainly used for references.
    void Awake()
    {
        //setting up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start ()
    {
		//The previous frame had the current frame camera posiotion
	    previousCameraPosition = cam.position;

        //assigning coressponding parallaxScales
        parallaxScales = new float[backgrounds.Length];

	    for (int i = 0; i < backgrounds.Length; i++)
	    {
	        parallaxScales[i] = backgrounds[i].position.z*-1;

	    }

	}
	
	// Update is called once per frame
	void Update ()
    {
		//for each background
	    for (int i = 0; i < backgrounds.Length; i++)
	    {
	        //the parallaxed is the oposite of the camera movement because the previous frame multiplied by scale
	        float parallax = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];

            //seting a target x position which is the current position plus the parallax
	        float backgroundTargetPositionX = backgrounds[i].position.x + parallax;

            //create a target postion which is background`s current postion with target x position
            Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and target position using lerp
	        backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPosition,
	            smoothing + Time.deltaTime);
            }
        //set previousCameraPosition to the Camera`s position at the end of the frame
	    previousCameraPosition = cam.position;

	}
}
