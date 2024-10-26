using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionCollider : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject collisionObject;
    GameObject indicator;
    GameObject textbox;
    GameObject player;
    Collider playerCollider;
    void Start()
    {
        //initialize objects
        print("start");
        player = GameObject.Find("playerCharacter");
        indicator = GameObject.Find("indicator");
        textbox = GameObject.Find("inspectTextbox");
        playerCollider = gameObject.GetComponent<Collider>();
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
            indicator.transform.Rotate(0, 20 * Time.deltaTime, 0);

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
        if (other.gameObject.tag != "Interactable")
        {
            print("ignore this one");
            Physics.IgnoreCollision(playerCollider, other);
        }
        print("collide start with");
        print(other.name);
        //check if other is interactable
        if(other.tag == "Interactable")
        {
            print("collide2");
            collisionObject = other.gameObject;
        } else
        {
            print(other.tag);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print("collide exit");
        //check if other is interactable
        if (other.tag == "Interactable")
        {
            collisionObject = null;
        }
    }
}
