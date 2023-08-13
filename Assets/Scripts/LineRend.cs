using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRend : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    LineRenderer lineRenderer;
    Transform[] points = {};


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////
    public void DrawLine(Transform[] points){
        Debug.Log("Drawing a line...");
        lineRenderer.positionCount = points.Length;
        this.points = points;

        for(int i = 0; i < points.Length; i++){
            lineRenderer.SetPosition(i, points[i].position);
        }        
    }
}
