/*
Title: TriggerColor.cs
Author: Soumitra Goswami
Date: 11/21/2018
Description: A Simple Trigger that changes the color of the material whenever a player enters the region and
             Changes back to it's original color when player exits the region.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColor : MonoBehaviour {
    //These Private variables do not show up as editable in the editor.
    private Color old_Color;//Variable to store the existing Color
    private Renderer render;//Required to access the material of the object

    //OnTriggerEnter is a Unity3D function which gets called when a Collider enters the Trigger region
    void OnTriggerEnter(Collider other)//other is the collider entering the region
    {
        render = GetComponent<Renderer>();//We initialize the component as a Renderer.
        old_Color = render.material.color;// We get the color of the material.
        if (other.tag == "Player")//Check if the Collider that has entered is a Player.
        {
            render.material.color = Color.green;//Change the color of the material to green whenever a player enters.
        }
    }

    //OnTriggerExit is a Unity3D function which gets called when a Collider leaves the Trigger region
    void OnTriggerExit(Collider other)
    {
        render = GetComponent<Renderer>();//Initializing the Renderer Component
        if (other.tag == "Player")//Check if the Collider leaving is a Player
        {
            render.material.color = old_Color;//Change it back to the original Color
        }
    }
}
