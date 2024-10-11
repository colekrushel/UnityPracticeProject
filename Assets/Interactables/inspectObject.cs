using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inspectObject : MonoBehaviour  
{
    MeshRenderer indicator;
    GameObject parent;
    GameObject textbox;
    private void Start()
    {
        parent = transform.parent.gameObject;
        indicator = GetComponent<MeshRenderer>();
        indicator.enabled = false;
        textbox = GameObject.Find("inspectTextbox");
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
            //check for interaction key
            if (Input.GetKeyDown("e"))
            {
                print("e pressed");
                
                if (textbox != null)
                {
                    //toggle textbox
                    textbox.SetActive(!textbox.activeSelf);
                    
                }
                else
                {

                    print("where the heck is the textbox?");
                }
            }
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
            //hide textbox when player leaves
            textbox.SetActive(false);
        }
    }
}
