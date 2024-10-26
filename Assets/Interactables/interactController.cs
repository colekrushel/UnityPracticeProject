using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactController : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.objectEnter += EventManager_objectEnter;
    }

    private void OnDisable()
    {
        EventManager.objectEnter -= EventManager_objectEnter;
    }

    private void EventManager_objectInspect()
    {
        throw new System.NotImplementedException();
    }
    private void EventManager_objectEnter()
    {
        // start listening for interacts and leaving
        EventManager.objectInspect += EventManager_objectInspect;
        EventManager.objectLeave += EventManager_objectLeave;
        // display inspector above player
    }

    private void EventManager_objectLeave()
    {
        // stop listening for interacts and leaving
        EventManager.objectInspect -= EventManager_objectInspect;
        EventManager.objectLeave -= EventManager_objectLeave;
        // disable inspector
    }

   



}
