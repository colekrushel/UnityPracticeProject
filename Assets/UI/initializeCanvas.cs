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
        //hide canvas by default
        textbox.SetActive(false);
        textcontent.text = "i am a textbox";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
