/*
Title: Gizmo.cs
Date: 11/21/2018
Description: Generates a viewable Transform Gizmo to signify a location of an empty object. 

Input: @float - gizmoSize: Size of the Gizmo
       @Color - gizmoColor: Color of the Gizmo

*/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour {

    [SerializeField] private float gizmoSize = 0.5f;
    [SerializeField] private Color gizmoColor = Color.yellow;
    
    //This specifies Gizmo UI being drawn onto the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
