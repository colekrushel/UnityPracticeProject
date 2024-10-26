using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class inspectObject : MonoBehaviour
{
    GameObject indicator;
    [SerializeField] GameObject textbox;
    GameObject player;
    MeshRenderer indicatorDisplay;
    //float pressDelay = 0.5f;
    //float lastPressedTime = 0f;
    private void Start()
    {
        indicator = GameObject.Find("indicator");
        player = GameObject.Find("playerCharacter");
        textbox = GameObject.Find("inspectTextbox");
        indicatorDisplay = indicator.GetComponent<MeshRenderer>();
        indicatorDisplay.enabled = false;



    }
    private void Update()
    {
        //if enabled then rotate and display
        if (indicatorDisplay.enabled)
        {
            //keep the indicator above the player
            indicator.transform.position = player.transform.position + new Vector3(0, 3, 0);
            indicator.transform.Rotate(0, 20 * Time.deltaTime, 0);
            //check for interaction key
            if (Input.GetKeyDown("e"))
            {
                print("e pressed");
                //only allow user to toggle every certain amount of time
                //if (lastPressedTime + pressDelay > Time.unscaledTime)
                //{
                //    return;
                //}
                //lastPressedTime = Time.unscaledTime;     
                if (textbox != null)
                {
                    //toggle textbox
                    bool textboxState = textbox.activeInHierarchy;
                    print("set active");
                    textbox.SetActive(!textboxState);
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
        if (other.tag == "Player")
        {
            EventManager.OnObjectInteract();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.OnObjectLeave();
        }
    }
}


