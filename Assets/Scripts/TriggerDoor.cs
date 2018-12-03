/*
Title: TriggerDoor.cs
Author: Soumitra Goswami
Date: 11/21/2018
Description: A Simple Trigger that runs the open and close Animation of the door whenever the player presses 'E'. Requires player to set up 
             the Animator sequence, where an open and close door animation is specified and conditions for the transitions are set.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour {

    //Public Variables are visible to change in the editor.
    public Animator _animator; //Animator sequence to be used.
    public Color m_Color; //Color of the material the user wants.
    public GameObject Door; //Door GameObject Variable used to get the material of the door.


    private Renderer render; //Required to access the material of the object
    private Color old_Color; //Variable to store the existing Color

    private bool isOpen = false;  // Variable to track whether our door is open or closed.
    private Material m;//Variable to store the material of the door.

    // Awake is a Unity3D function that gets called at the game start.
    void Awake()
    {
        // We initialize the values 
        render = GetComponent<Renderer>();
        m = Door.GetComponent<Renderer>().material; //Assign the Door Material to a variable
        old_Color = m_Color;//Store the color specified
        render.material.color = m_Color; //Apply the color specified by the player
        m.color = m_Color;//Apply the door color to the color specified by the player
       
    }

    //OnTriggerStay is a Unity3D function which gets called every frame a Collider is inside the Trigger Volume.
    void OnTriggerStay(Collider other)//other is the collider entering the region.
    {
        //Check to see "if the Collider is the Player" AND "if The player pressed the E key" AND "if the door is not Open."
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            render.material.color = Color.green; //Change the color of the trigger to Green
            m.color = Color.green; // Set the door color to Green
            _animator.SetBool("open", true); //Set the animation parameter to true. (Runs the open animation)
            isOpen = true; //Now that the door is open. We set isOpen to be true.
        }

        //Check to see "if the Collider is the Player" AND "if The player pressed the E key" AND "if the door is Open."
        else if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            render.material.color = old_Color; //Change back the color of the material to it's oldColor
            _animator.SetBool("open", false); //Set the animation parameter to be false. (Runs the close Animation)
            m.color = old_Color;// Set the door color to old_Color
            isOpen = false;// Now that the door is closed, we set iOpen to be false.
        }
    }
}
