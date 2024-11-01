using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInfo
{
    public static Dictionary<string, string> objectDescs = new Dictionary<string, string>()
    {
        { "Pyramid", "what a strange looking pyramid" },
        { "BlueRect", "this rectangle is very blue" }
    };
    public static Dictionary<string, Vector3> teleLocations = new Dictionary<string, Vector3>()
    {
        { "PyramidArch", new Vector3(0, 0, 0)  }
        
    };
    public static Dictionary<string, string> sceneLocations = new Dictionary<string, string>()
    {
        { "PyramidArch", "PyramidScene" },
        { "PyramidDoor", "SampleScene" }

    };


}
