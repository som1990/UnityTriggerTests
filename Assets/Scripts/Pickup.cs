/*
Title: Pickup.cs
Author: Soumitra Goswami
Date: 11/22/2018
Description: Enables the player to pick up objects with the tag "Pickable". Attach this to the Player Controller. 
             You will need to create an Empty Game OBject (Child to the playerController) which will carry the location of the pickup object

*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    //Public variables show up in the editor to be edited.
    //public bool camHold = true;
    public Camera cam; // Camera we are using to get the direction to shoot the ray.
    public GameObject tempParent; // We attach our pickup object to this GameOBject.(In our case it's the "PickupObjectPivot" 
    public float playerDistanceFromCam = 2.0f; //We offset the ray to shoot from front of the player based on this distance.

    //Private variables do not show up in the editor unless specified specifically. (Check "ThirdPersonCamera"   Script to find out how.)
    private Vector3 objectPos; //Tracks the position of the cube in the world.
    private bool isHoldingObject = false;// Variable determining if player is holding the object or not
    private float maxDistance = 500.0f; // Max Search Distance to check collision

    //Local Variables. 
    private Rigidbody rigid;
    private RaycastHit hit;
    private GameObject pickedUpObject;

    //Start is a Unity3D function that gets called whenever this script gets activated.
    void Start()
    {
        pickedUpObject = null;
    }

    //Update is a Unity3D function that gets called once every frame.
    void Update()
    {
        if (!isHoldingObject)//Checks to see if we are not currently holding any object.
        {

            if (Input.GetKeyDown(KeyCode.F))//Checks to see if the player presses "F" 
            {
                //Debuging by printing values in the console.
                //Debug.Log("Pressed F");
                //Debug.Log(cam.transform.position);

                //We need a ray. Ray has an origin and a direction.
                Vector3 origin = cam.transform.position; //World location of where to shoot the ray.
                Vector3 direction = cam.transform.forward; //Direction of the ray we are shooting.
                origin = origin + direction * playerDistanceFromCam;//We want the ray to begin a little further out from the player 
                Debug.DrawRay(origin, direction * maxDistance, Color.green, 4);//Draw ray in the editor to visualize our ray.
                if (Physics.Raycast(origin, direction, out hit))//Checking to see if we hit anything. "out" tells us that we are recieving data from the function.
                {
                    //Printing out the tag of the object we hit
                    Debug.Log("We hit this tag: " + hit.collider.gameObject.tag);
                    if (hit.collider.gameObject.tag == "Pickable")//We only want to pick up elements with the tag "Pickable"
                    {
                        Debug.Log("We hit the pickup object");//Another console print to say we hit the object.
                        pickedUpObject = hit.collider.gameObject;//we get the game object of the collider
                        if (hit.rigidbody)//We want the object to have a rigidbody Physics Collider for this to work.
                        {
                            pickedUpObject.transform.SetParent(tempParent.transform);//We move our object under the "PickupObjectPivot" GameObject we created in the editor.
                            pickedUpObject.transform.position = tempParent.transform.position;//we set the location of object to the location of the "PickupObjectPivot" GameObject.
                            //We don't want our object to use gravity or collide with anything when it's hovering in front of us.
                            hit.rigidbody.useGravity = false;
                            hit.rigidbody.detectCollisions = false;
                            isHoldingObject = true; //Now that we are holding our object we set this to true.
                        }
                    }
                }
            }

            

        }
    
        else //If we are holding an object
        {
            if (Input.GetKeyDown(KeyCode.F)) //Checks to see If the player presses the F Key.
            {
                if (pickedUpObject != null)//Safeguard to see if we are not accidently inside. (Not really necessary since we only enter here if the player is holding something)
                {
                    objectPos = pickedUpObject.transform.position;//Gathers the world position of our object at the time of key press.
                    pickedUpObject.transform.SetParent(null);//We Detach our Object from the "PickupObjectPivot" GameOBject
                    pickedUpObject.transform.position = objectPos;//We place our detached object in the world position of where it was attached
                    hit.rigidbody.useGravity = true; //We make the object active so it can fall via gravity
                    hit.rigidbody.detectCollisions = true; // We allow it to collide with other objects
                    isHoldingObject = false; // Now that we aren't holding anything we set isHoldingObject to false.
                    pickedUpObject = null; // we delete the variable by making it null
                }
                
            }
            else if (hit.rigidbody)//If the object we are holding has a rigidbody.
            {
                hit.rigidbody.velocity = Vector3.zero;//We make sure our object is static in front of us
                hit.rigidbody.angularVelocity = Vector3.zero;//We make sure our object isn't rotating in front of us.
                pickedUpObject.transform.position = tempParent.transform.position; //We update the position of the object to location of the parent.
             }
        }
        
       

    }
}
