using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class initializeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    //Canvas canvas;
    GameObject textbox;
    [SerializeField] TMP_Text textcontent;
    void Start()
    {
        textbox = gameObject;
        textcontent.text = "i am a textbox";
        //hide canvas by default
        textbox.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
