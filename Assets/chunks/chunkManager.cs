using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class chunkManager : MonoBehaviour
{
    //Class to keep track of the chunk layout, and handles loading in new chunks and temporarily unloading unneeded ones
    //all chunks are the same size: 100 x 100
    [SerializeField] GameObject Room1;
    [SerializeField] GameObject Room2;
    [SerializeField] private static List<List<GameObject>> chunkMap;
    [SerializeField] int zeroIndexX; //negative direction left
    [SerializeField] int zeroIndexY; //negative direction north
    [SerializeField] int width;
    [SerializeField] int height;
    
    void Start()
    {
        print("chunk manager start");
        zeroIndexX = 0; zeroIndexY = 0;
        width = 1; height = 1;
        //instantiate chunkmap and add starting room
        GameObject loadedChunk = Instantiate(Room1, this.transform);
        List<GameObject> axis0 = new List<GameObject>();
        axis0.Add(loadedChunk);
        chunkMap = new List<List<GameObject>>();
        chunkMap.Add(axis0);
        //move player to center of room
        GameObject player = GameObject.Find("playerCharacter");
        player.transform.position = loadedChunk.transform.position;
        
    }


    private void adjustGrid(string expansionDirection)
    {
        print("adjusted");
        //make all rows and columns match the max width and height, filling unexplored chunks with nulls
        switch (expansionDirection)
        {
            case "R":
                for (int i = 0; i < chunkMap.Count; i++)
                {
                    //add a null to the end of every smaller row
                    if (chunkMap[i].Count < width)
                    {
                        chunkMap[i].Add(null);
                    }
                 }
                break;

            case "L":
                for (int i = 0; i < chunkMap.Count; i++)
                {
                    //add a null to the beginning of every smaller row
                    if (chunkMap[i].Count < width)
                    {
                        chunkMap[i].Insert(0, null);
                    }
                }
                break;
        }

    }

    private void adjustRow(int xPos, ref List<GameObject> inputRow)
    {
        //add null gameobjects before and after the item in the grid
        //if xpos is 3, zeroindexX is 2 and width is 7, then add 5 nulls before (zeroindexX + xpos ) and 1 after (width - prev val - 1)
        
        //before the chunk
        for(int i = 0; i < (xPos + zeroIndexX ); i++)
        {
            inputRow.Insert(0, null);
        }

        //after the chunk
        for (int i = 0; i < (width - (xPos + zeroIndexX ) - 1); i++)
        {
            inputRow.Add(null);
        }
    }


    // chunk methods

    //determine what chunk to load

    private GameObject chooseChunk()
    {
        int rNum = Random.Range(0,10);
        if (rNum < 9)
        {
            return Room1;
        } else if (rNum <= 10)
        {
            return Room2;
        } else
        {
            return Room1;
        }
        
    }


    // method to load in new chunk
    public void loadChunk (string entranceDirection, int x, int y)
    {
        //print("load chunk");
        //print(entranceDirection);
        //get chunk from x and y
        GameObject currChunk = chunkMap[y + zeroIndexY][x + zeroIndexX];
        Vector3 currChunkPos = currChunk.transform.position;
        //load new chunk
        GameObject loadedChunk = Instantiate(chooseChunk(), this.transform);
        
        switch (entranceDirection)
        {
            case "R": //positive x
                //move chunk right of current chunk
                loadedChunk.transform.position = currChunkPos + new Vector3(100, 0, 0);
                //check if chunk is loaded into a null placeholder or will expand the grid
                if ((x + 1 + zeroIndexX) >= width)
                {
                    
                    //chunk will expand the grid

                    //add new chunk to chunkmap
                    chunkMap[y + zeroIndexY].Add(loadedChunk);
                    width += 1;
                    adjustGrid(entranceDirection);
                } else
                {
                    //chunk is loaded into a placeholder
                    chunkMap[y + zeroIndexY][x + zeroIndexX + 1] = loadedChunk;

                }
                break;

            case "L": //negative x
                //move chunk left of current chunk
                loadedChunk.transform.position = currChunkPos - new Vector3(100, 0, 0);
                //check if chunk is loaded into a null placeholder or will expand the grid
                if (Mathf.Abs(x-1) > zeroIndexX)
                {
                    //chunk will expand the grid

                    //add new chunk to chunkmap
                    chunkMap[y + zeroIndexY].Insert(0, loadedChunk);
                    //update zeroindexX
                    zeroIndexX += 1;
                    width += 1;
                    adjustGrid(entranceDirection);
                }
                else
                {
                    //chunk is loaded into a placeholder
                    chunkMap[y + zeroIndexY][x + zeroIndexX - 1] = loadedChunk;

                }
                break;

            case "D": //positive y
                //move chunk below current chunk
                loadedChunk.transform.position = currChunkPos - new Vector3(0, 0, 100);
                //check if chunk is loaded into a null placeholder or will expand the grid
                if ((y + 1 + zeroIndexY) >= height)
                {
                    //chunk will expand the grid
                    //new chunk will be contained in a new array added to the chunkMap
                    List<GameObject> newRow = new List<GameObject>();
                    newRow.Add(loadedChunk);
                    adjustRow(x, ref newRow);
                    //add new chunk to end of chunkmap
                    chunkMap.Add(newRow);
                    height += 1;
                }
                else
                {
                    //chunk is loaded into a placeholder
                    chunkMap[y + zeroIndexY + 1][x + zeroIndexX] = loadedChunk;

                }
                break;
            case "U": //negative y
                //move chunk below current chunk
                loadedChunk.transform.position = currChunkPos - new Vector3(0, 0, -100);
                //check if chunk is loaded into a null placeholder or will expand the grid
                if (Mathf.Abs(y-1) > zeroIndexY)
                {
                    //chunk will expand the grid
                    //new chunk will be contained in a new array added to the chunkMap
                    List<GameObject> newRow = new List<GameObject>();
                    newRow.Add(loadedChunk);
                    adjustRow(x, ref newRow);
                    //add new chunk to beginning of chunkmap
                    chunkMap.Insert(0, newRow);
                    height += 1;
                    zeroIndexY += 1;
                }
                else
                {
                    //chunk is loaded into a placeholder
                    chunkMap[y + zeroIndexY - 1][x + zeroIndexX] = loadedChunk;

                }
                break;

            default:

                break;
        }
    }

    // public method to get chunk player is in
    public Vector3 getCurrChunk (Vector3 inputPos)
    {
        //inverse player's z because i am stubborn
        //inputPos.z *= -1;
        //chunks are 100x100 blocks, beginning at chunkManager's position.
        //compare input position to chunkManager's position to find difference in x and z.

        //add 50 to player's pos, and divide by 100 to get x and y index
        //add 1 tile to negative position to accomodate how index is calculated
        if ((inputPos.x + 50) < 0) inputPos.x -= 100;
        if ((inputPos.z + 50) < 0) inputPos.z -= 100;
        int xIndex = (int) (inputPos.x + 50) / 100;
        int yIndex = (int) (inputPos.z + 50) / 100;
        //print(xIndex);
        //print(yIndex);
 
        return new Vector3 ((xIndex * 100), 0, (yIndex * 100));
    }

    // public method to check if a chunk exists at coordinates

    public bool isChunk(int x, int y)
    {
        if (x + zeroIndexX >= width || y + zeroIndexY >= height) //out of bounds x right & y bottom
        {
            return false;
        }
        else if(x < (zeroIndexX * -1) || y < (zeroIndexY * -1)) // out of bounds x left & y top
        {
            return false;
        } 
        else if (chunkMap[y + zeroIndexY][x + zeroIndexX] != null)
        {
            return true;
        }
        else if(chunkMap[y + zeroIndexY][x + zeroIndexX] == null) //if null then chunk does not already exist
        {
            return false;
        }
        return false;
    }

    
}
