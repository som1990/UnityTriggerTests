/*
Title: TriggerKey_Light.cs
Author: Soumitra Goswami
Date: 11/21/2018
Description: A Simple Trigger that Toggles the color of the material and visibility of a light whenever Player 
             presses the Key 'E' 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKey_Light : MonoBehaviour {

    //Public Variables are visible to change in the editor.
    public Light light;//Light we want to affect
    public Color m_Color;//Color of the material the user wants.

    //These Private Variables are not visible to change in the editor 
    private bool isLightOn = false; // Variable to track whether our light is on or off.
    private Renderer render;//Required to access the material of the object
    private Color old_Color;//Variable to store the existing Color


    // Awake is a Unity3D function that gets called at the game start.
    void Awake()
    {
        // We initialize the values 
        render = GetComponent<Renderer>();
        old_Color = m_Color; //Store the color specified
        render.material.color = m_Color; //Apply the color specified by the player
        light.color = Color.green; //Change the color of the light to Green.
        light.enabled = false; //We want the light to be OFF at the start
    }
    //OnTriggerStay is a Unity3D function which gets called every frame a Collider is inside the Trigger Volume.
    void OnTriggerStay(Collider other)//other is the collider entering the region.
    {
        //Check to see "if the Collider is the Player" AND "if The player pressed the E key" AND "if the light is not on."
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !isLightOn) 
        {

            render.material.color = Color.green;//Change the color to green
           
            light.enabled = true; //Switch ON the light
            isLightOn = true; //Now that the light is ON we set isLightOn to be true 
        }
        //Check to see "if the Collider is the Player" AND "if The player pressed the E key" AND "if the light is not on."
        else if ( other.tag == "Player" && Input.GetKeyDown(KeyCode.E) && isLightOn)
        {
            render.material.color = old_Color;//Change back the color of the material to it's oldColor
            light.enabled = false;//Switch OFF the light
            isLightOn = false;//Now that the light is OFF we set isLightOn to false.
        }
    }
}
