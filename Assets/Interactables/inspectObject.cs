using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inspectObject : MonoBehaviour  
{
    MeshRenderer indicator;
    GameObject parent;
    private void Start()
    {
        parent = transform.parent.gameObject;
        indicator = GetComponent<MeshRenderer>();
        indicator.enabled = false;
        //set pos of indicator above parent
        float height = parent.GetComponent<MeshRenderer>().bounds.size.y;
        indicator.transform.position = new Vector3(indicator.transform.position.x, height, indicator.transform.position.z);
    }
    private void Update()
    {
        //if enabled then rotate 

        if (indicator.enabled)
        {
            indicator.transform.Rotate(0, 1, 0);
        }

    }
    //trigger when player enters bounding box
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("triggered");
            
           
            indicator.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print("triggered2");
        if (other.tag == "Player");
        {
            indicator.enabled = false;
        }
    }
}
