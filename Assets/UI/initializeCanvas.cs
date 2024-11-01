using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class initializeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject textbox;
    [SerializeField] TMP_Text textcontent;
    void Start()
    {
        textbox = gameObject;
        textcontent.text = "i am a textbox";
        //hide canvas by default - DONT DO THIS THE CODE CANT FIND INACTIVE OBJECTS
        //textbox.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
