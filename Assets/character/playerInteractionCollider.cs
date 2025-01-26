using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInteractionCollider : MonoBehaviour
{
    //options
    const int loadDist = 10;


    // Start is called before the first frame update
    GameObject collisionObject;
    GameObject indicator;
    GameObject textbox;
    GameObject player;
    chunkManager chunkSystem;
    [SerializeField] GameObject destinationRoom;
    Collider playerCollider;
    Dictionary<string, string> desc;
    Dictionary<string, Vector3> locs;
    Dictionary<string, string> roomLocs;
    Dictionary<string, GameObject> rooms;
    

    int chunkX;
    int chunkY;
    void Start()
    {
        chunkX = 0;
        chunkY = 0;
        //initialize objects
        print("start");
        player = gameObject;
        indicator = GameObject.Find("indicator");
        textbox = GameObject.Find("inspectTextbox");
        destinationRoom = GameObject.Find("DefaultRoom");
        //set textbox deactive by default
        textbox.SetActive(false);
        //get components
        playerCollider = gameObject.GetComponent<Collider>();
        chunkSystem = GameObject.Find("ChunkManager").GetComponent<chunkManager>();
        //read in dictionary info
        desc = objectInfo.objectDescs;
        locs = objectInfo.teleLocations;
        roomLocs = objectInfo.roomLocations;
        rooms = objectInfo.rooms;
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
        //check if player is a certain distance from the chunk border; if so, then load in an appropriate new chunk

        //get center of current chunk
        Vector3 relativePos = player.transform.position - chunkSystem.transform.position;

        //print(relativePos);
        Vector3 currChunkPos = chunkSystem.getCurrChunk(relativePos);
        //update player chunkX and chunkY
        chunkX = (int) currChunkPos.x / 100;
        chunkY = (int) currChunkPos.z / 100 * -1;
        //get difference between player pos and chunk pos
        //print(relativePos);
        //print(currChunkPos);
        Vector3 diff = relativePos - currChunkPos;
        //print(chunkX);
        //print(chunkY);
        //print(diff);
        if (diff.x > loadDist)
        {
            //dont load chunk if already exists
            if (chunkSystem.isChunk(chunkX+1, chunkY))
            {
                //dont load
            } else
            {
                chunkSystem.loadChunk("R", chunkX, chunkY);
            }
            
        } if(diff.x < loadDist * -1) {
            if (chunkSystem.isChunk(chunkX-1, chunkY))
            {
                //dont load
                
            } else
            {
                chunkSystem.loadChunk("L", chunkX, chunkY);
            }
            
        } if (diff.z < loadDist * -1)
        {
            if(chunkSystem.isChunk(chunkX, chunkY + 1))
            {
                //dont load
                
            } else
            {
                chunkSystem.loadChunk("D", chunkX, chunkY);
            }
        } if(diff.z > loadDist)
        {
            if (chunkSystem.isChunk(chunkX, chunkY - 1))
            {
                //dont load
                
            }
            else
            {
                chunkSystem.loadChunk("U", chunkX, chunkY);
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
