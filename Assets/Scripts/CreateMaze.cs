using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateMaze : MonoBehaviour
{   
    public Material floorMaterial;
    public Material wallMaterial;

    GameObject maze;
    string mazeTag;

    void Start()
    {
        mazeTag = "Maze";

        // Transform is always added to the GameObject that is being created. 
        // The creation of a GameObject with no script arguments will add the Transform but nothing else.
        maze = new GameObject("MyMaze");
        maze.tag = mazeTag; // assumes tag exists

        CreateMazeObject();
    }

    void CreateMazeObject ()
    {
        // lower left reference point
        float x = 0; 
        float y = 0; 
        float z = 0;

        string floor = "floor";
        string wall  = "wall";

        // floor
        CreateCubeWithLowerLeftRect(new Vector3(12,1,12), new Vector3(x,y,z), floorMaterial, floor); // rgba

        // walls
        CreateCubeWithLowerLeftRect(new Vector3(3, 1, 1), new Vector3(x   , y+1, z+ 1), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 4), new Vector3(x+ 4, y+1, z   ), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 3), new Vector3(x+ 6, y+1, z+ 1), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(4, 1, 1), new Vector3(x+ 7, y+1, z+ 1), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(2, 1, 1), new Vector3(x+ 1, y+1, z+ 3), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 5), new Vector3(x+ 8, y+1, z+ 3), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(2, 1, 1), new Vector3(x+10, y+1, z+ 3), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 2), new Vector3(x+ 2, y+1, z+ 4), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 6), new Vector3(x+10, y+1, z+ 4), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 2), new Vector3(x   , y+1, z+ 5), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(4, 1, 1), new Vector3(x+ 3, y+1, z+ 5), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 3), new Vector3(x+ 3, y+1, z+ 6), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(4, 1, 1), new Vector3(x+ 5, y+1, z+ 7), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(2, 1, 1), new Vector3(x+ 1, y+1, z+ 8), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 4), new Vector3(x+ 6, y+1, z+ 8), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(1, 1, 2), new Vector3(x+ 1, y+1, z+ 9), wallMaterial, wall);
        CreateCubeWithLowerLeftRect(new Vector3(3, 1, 1), new Vector3(x+ 8, y+1, z+ 9), wallMaterial, wall);
                                                                                                    
        CreateCubeWithLowerLeftRect(new Vector3(3, 1, 1), new Vector3(x+ 2, y+1, z+10), wallMaterial, wall);
    }

    void CreateCubeWithLowerLeftRect( Vector3 size, Vector3 position, Material material, string name)
    {
        float xCenter = position.x;
        float yCenter = position.y;
        float zCenter = position.z;

        float xSize = size.x;
        float ySize = size.y;
        float zSize = size.z;

        // lower left reference point
        float x = xCenter + xSize / 2; 
        float y = yCenter + ySize / 2; 
        float z = zCenter + zSize / 2; 

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = size;
        cube.transform.position = new Vector3(x,y,z);
        cube.GetComponent<Renderer>().material = material;

        cube.name = name;
        //cube.tag = mazeTag;

        cube.transform.SetParent(maze.transform);
    }
}
