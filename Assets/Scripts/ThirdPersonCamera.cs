/*
Title: ThirdPersonCamera.cs
Author: Soumitra Goswami
Date: 11/21/2018
Description: A Simple Third Person Follow Camera script. Attach it to the Main Camera.
             
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ThirdPersonCamera : MonoBehaviour
{
    //Constants
    private const float Y_ANGLE_MIN = 20.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    //These Private Variables are visible to change in the editor.
    //[SerializeField] Allows variables to be private to the outside scripts and yet allow you to change variables in the editor
    [SerializeField] protected Transform m_Target; //Camera Target to Look at and follow.
    [SerializeField] private bool m_AutoTargetPlayer = true; //True if it automatically finds a Player Character to lock on to.
    [SerializeField] private Transform camTransform; // Camera transform.
    [SerializeField] private float distanceFromPlayer = 2.0f; //The Distance of the camera from the player. 
    [SerializeField] private float sensitivityX = 4.0f; // Horizontal Sensitivity
    [SerializeField] private float sensitivityY = 1.0f; // Vertical Sensitivity

    //These Private Variables are not visible to change in the editor 
    private Camera cam; // our camera

    private float currentX = 0.0f; //Current Mouse X Position
    private float currentY = 0.0f; //Current Mouse Y Position
   
    //Target Rigidbody Colider of the target player character
    protected Rigidbody targetRigidbody;

    //Start is a Unity3D function that gets called whenever this script gets activated.
    void Start()
    {
        if(m_AutoTargetPlayer)// if Autotargeting is set to true.
        {
            FindAndTargetPlayer();//We find the Player Controller and attach it to the script
        }
        if (m_Target == null) return; // If no Player Controller is found. We return null
        camTransform = transform;
        cam = Camera.main; //set our camera to be the main camera
        targetRigidbody = m_Target.GetComponent<Rigidbody>();// we get the rigidbody of our player controller
    }

    //Update is a Unity3D function that gets called once every frame.
    void Update()
    {
        currentX += Input.GetAxis("Mouse X")*sensitivityX; //Here we store the movement of our mouse in the X Axis
        currentY += Input.GetAxis("Mouse Y")*sensitivityY;// Here we store the movement of our mouse in the Y

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX); // We restrict Vertical movement.(So that we are always behind the player
    }

    //LateUpdate is a Unity3D function that gets called once every frame only after the Update functions are called. In our case
    //we want to update the camera position only after the player has moved that frame. 
    void LateUpdate()
    {
        //If AutotargetPlayer is true and we have not found the player controller yet or 
        //If the target does not have a rigidbody, or - does have a rigidbody but is set to kinematic.
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindAndTargetPlayer();
        }
        Vector3 dir = new Vector3(0, 0, -distanceFromPlayer);//Get the direction from the point behind the player 
        Vector3 lookAtOffset = new Vector3(0, 1, 0); // Vertical offset of the camera.
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0); // Rotate the camera around X axis(Vertical Movement) and Y axis(Horizontal Movement)  based on mouse movements.
        camTransform.position = m_Target.position+lookAtOffset + rotation * dir; // Set the position of the camera directly behind the PlayerController.
        camTransform.LookAt(m_Target.position + lookAtOffset); // Have the camera look at the player at all times.
    }

    //Function to find the player controller with the tag "Player"
    public void FindAndTargetPlayer()
    {
        var targetObj = GameObject.FindGameObjectWithTag("Player"); // Find the Game object with tag "Player"
        if (targetObj) // If the player Game object exists.
        {
            SetTarget(targetObj.transform); // setthe target variable to the PLayer Controller
        }
    }

    //Function to set the Target variable.
    public virtual void SetTarget(Transform newTransform)
    {
        m_Target = newTransform;
    }

}
