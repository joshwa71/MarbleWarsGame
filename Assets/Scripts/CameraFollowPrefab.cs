using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;


public class CameraFollowPrefab : MonoBehaviour
{

    GameObject player;

    //[Tooltip("The distance in the local x-z plane to the target")]
    //[SerializeField]
    //private float distance = 7.0f;


    //[Tooltip("The height we want the camera to be above the target")]
    //[SerializeField]
    //private float height = 3.0f;


    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;


    [Tooltip("The Smoothing for the camera to follow the target")]
    [SerializeField]
    private float smoothSpeed = 0.125f;


            // cached transform of the target

    Transform camTransform;
    Transform camTransform1;
    Transform camTransform2;
    Transform camTransform3;


            // Cache for camera offset
    Vector3 cameraOffset = Vector3.zero;



    void Start() 
    {

        camTransform = this.transform.GetChild(0).gameObject.transform;
        camTransform1 = this.transform.GetChild(1).gameObject.transform;
        camTransform2 = this.transform.GetChild(2).gameObject.transform;
        camTransform3 = this.transform.GetChild(3).gameObject.transform;

        Follow(camTransform, 0, 12, 20);
        Follow(camTransform1, 0, -12, 20);
        Follow(camTransform2, 12, 0, 20);
        Follow(camTransform3, -12, 0, 20);

    }

    // Update is called once per frame
    void Update()
    {
        Follow(camTransform, 0, 12, 20);
        Follow(camTransform1, 0, -12, 20);
        Follow(camTransform2, 12, 0, 20);
        Follow(camTransform3, -12, 0, 20);
    }

    void Follow(Transform cameraTransform, float width, float distance, float height)
    {
        cameraOffset.x = width;
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed*Time.deltaTime);

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }
}
