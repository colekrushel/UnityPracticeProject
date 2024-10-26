using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerInteractionCollider : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject collisionObject;
    GameObject indicator;
    GameObject textbox;
    GameObject player;
    Collider playerCollider;
    Dictionary<string, string> desc;
    Dictionary<string, Vector3> locs;
    void Start()
    {
        //initialize objects
        print("start");
        player = gameObject;
        indicator = GameObject.Find("indicator");
        textbox = GameObject.Find("inspectTextbox");
        playerCollider = gameObject.GetComponent<Collider>();
        desc = objectInfo.objectDescs;
        locs = objectInfo.teleLocations;
        //ignore collision between player and this collision box
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());

    }

    // Update is called once per frame
    void Update()
    {
        //if in collision with another object
        if (collisionObject != null)
        {
            //keep the indicator above the player
            indicator.transform.position = player.transform.position + new Vector3(0, 3, 0);
            indicator.transform.Rotate(0, 200 * Time.deltaTime, 0);

            //check for interaction key
            if (Input.GetKeyDown("e"))
            {
                print("e pressed");
                if (textbox != null)
                {
                    //toggle textbox
                    bool textboxState = textbox.activeInHierarchy;
                    print("set active");
                    textbox.SetActive(!textboxState);
                    //read text specific to object
                    string cname = collisionObject.name;
                    textbox.GetComponentInChildren<TMP_Text>().text = desc[cname];
                }
                else
                {
                    print("where the heck is the textbox?");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //stop colliding with non interactable objects
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(playerCollider, other);
        }

        //check if other is interactable
        if (other.tag == "Interactable")
        {
            collisionObject = other.gameObject;
        }
        if (other.tag == "Teleporter")
        {
            print("tele");
            //move player according to the teleporter's location in the dictionary
            player.transform.position = locs[other.name];
        }
        print(other.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        print("collide exit");
        //check if other is interactable
        if (other.tag == "Interactable")
        {
            collisionObject = null;
            //move indicator out of the field of view
            indicator.transform.position = new Vector3(0, 0, 0);
            //disable textbox
            textbox.SetActive(false);
        }
    }
}
