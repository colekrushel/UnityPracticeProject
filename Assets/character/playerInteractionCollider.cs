using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInteractionCollider : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject collisionObject;
    GameObject indicator;
    GameObject textbox;
    GameObject player;
    [SerializeField] GameObject destinationRoom;
    Collider playerCollider;
    Dictionary<string, string> desc;
    Dictionary<string, Vector3> locs;
    Dictionary<string, string> roomLocs;
    Dictionary<string, GameObject> rooms;
    Dictionary<string, string> loadZones;
    Dictionary<string, string> chunkDirs;
    string enteredFrom;
    void Start()
    {
        //initialize objects
        print("start");
        player = gameObject;
        indicator = GameObject.Find("indicator");
        textbox = GameObject.Find("inspectTextbox");
        destinationRoom = GameObject.Find("DefaultRoom");
        //set textbox deactive by default
        textbox.SetActive(false);
        playerCollider = gameObject.GetComponent<Collider>();
        desc = objectInfo.objectDescs;
        locs = objectInfo.teleLocations;
        roomLocs = objectInfo.roomLocations;
        rooms = objectInfo.rooms;
        loadZones = objectInfo.chunkTriggers;
        chunkDirs = objectInfo.chunkSides;
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
        if (other.tag == "Transporter")
        {
            print("tele");
            //move player according to the teleporter's location in the dictionary
            //disable prev room
            destinationRoom.SetActive(false);
            destinationRoom = rooms[roomLocs[other.name]];
            //enable room player is teleporting to
            destinationRoom.SetActive(true);

            //need to disable player controller in order to teleport
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = locs[other.name];
            player.GetComponent<CharacterController>().enabled = true;

            //disable indicator
            indicator.transform.position = new Vector3(0, 0, 0);
            //disable textbox
            textbox.SetActive(false);

        }
        if (other.tag == "Teleporter")
        {
            //need to disable player controller in order to teleport
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = locs[other.name];
            player.GetComponent<CharacterController>().enabled = true;

            //disable indicator
            indicator.transform.position = new Vector3(0, 0, 0);
            //disable textbox
            textbox.SetActive(false);
        }
        if (other.tag == "ChunkTrigger")
        {
            //load in chunk
            //get direction
            string direction = chunkDirs[other.name];
            //load prefab
            GameObject newChunk = rooms[loadZones[other.name]];
            //discriminate based on direction
            if (direction == "R")
            {
                //instantiate prefab to the right of the load zone
                Instantiate(newChunk, newChunk.transform.position, Quaternion.identity);
                //move prefab
                newChunk.transform.position = other.transform.position + new Vector3(33, 2, -2);
                //disable this loader as chunk is now loaded
                other.gameObject.SetActive(false);
                //disable the left loader on the new chunk
                //GameObject leftLoader = newChunk.Find("LeftLoader");
            }
            else if (direction == "L")
            {
                Instantiate(newChunk, newChunk.transform.position, Quaternion.identity);
                //move prefab
                newChunk.transform.position = other.transform.position - new Vector3(33, -2, 2);
                //disable this loader as chunk is now loaded
                other.gameObject.SetActive(false);
                //disable the right loader on the new chunk
            }
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
            //move indicator out of the field of view
            indicator.transform.position = new Vector3(0, 0, 0);
            //disable textbox
            textbox.SetActive(false);
        }
    }
}
