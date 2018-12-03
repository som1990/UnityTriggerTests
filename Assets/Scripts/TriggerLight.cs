/*
Title: TriggerLight.cs
Author: Soumitra Goswami
Date: 11/21/2018
Description: A Simple Trigger that Toggles the color of the material and visibility of a light whenever Player 
             enters amd exits the Trigger Volume. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerLight : MonoBehaviour {

    //Public Variables are visible to change in the editor.
    public Light light; //Light to be affected
    public Color m_Color; //Color to be assigned to the Trigger.

    //These Private variables do not show up as editable in the editor.
    private Renderer render; //Required to access the material of the object
    private Color old_Color; //Variable to store the existing Color

    // Awake is a Unity3D function that gets called at the game start.
    void Awake () {
        // We initialize the values 
        render = GetComponent<Renderer>();
        old_Color = m_Color; //Store the color specified
        render.material.color = m_Color; //Apply the color specified by the player
        light.color = Color.green; //Change the color of the light to Green.
        light.enabled = false;  //We want the light to be OFF at the start
    }

    //OnTriggerEnter is a Unity3D function which gets called when a Collider enters the Trigger region
    void OnTriggerEnter(Collider other)//other is the collider entering the region.
    {
        //Check to see if the collider is the "Player"       
        if(other.tag == "Player")
        {
           
            render.material.color = Color.green;//Change the color to green
            light.enabled = true; //Switch OFF the light
        }
    }

    //OnTriggerExit is a Unity3D function which gets called when a Collider leaves the Trigger region
    void OnTriggerExit(Collider other)
    {
        //Check to see if the collider is the "Player" 
        if (other.tag == "Player")
        {
            render.material.color = old_Color; //Change back the color of the material to it's oldColor
            light.enabled = false; //Switch OFF the light
        }
    }
}
