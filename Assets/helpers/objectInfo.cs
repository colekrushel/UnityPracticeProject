using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInfo
{
    public static Dictionary<string, string> objectDescs = new Dictionary<string, string>()
    {
        { "Pyramid", "what a strange looking pyramid" },
        { "BlueRect", "this rectangle is very blue" },
        { "StrangeCone", "what is this cone doing here?" },
        { "Lamp", "lamp." }
    };
    public static Dictionary<string, Vector3> teleLocations = new Dictionary<string, Vector3>()
    {
        { "PyramidArch", new Vector3(-51, -3, 42)  },
        { "PyramidDoor", new Vector3(0, 0, 0) },
        { "RightDoor", new Vector3(169-58, -2, 62) },
        { "LeftDoor", new Vector3(169, -2, 62) },
        { "StreetArch", new Vector3(126, -2, 62) }

    };
    public static Dictionary<string, string> roomLocations = new Dictionary<string, string>()
    {
        { "PyramidArch", "Room1" },
        { "PyramidDoor", "DefaultRoom" },
        { "StreetArch", "Room2" }

    };
    public static Dictionary<string, string> chunkTriggers = new Dictionary<string, string>()
    {
        { "RightDoor", "Room2" },
        { "LeftDoor", "Room2" }
    };
    public static Dictionary<string, string> chunkSides = new Dictionary<string, string>()
    {
        { "RightDoor", "R" },
        { "LeftDoor", "L" }
    };
    //references to all of the room game objects
    public static Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>()
    {
        {"DefaultRoom", GameObject.Find("DefaultRoom") },
        {"Room1", GameObject.Find("Room1") },
        {"Room2", GameObject.Find("Room2") }
    };


}
