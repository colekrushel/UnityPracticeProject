using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{

    public GameObject player;
    //public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 18, -12);
    }
}
