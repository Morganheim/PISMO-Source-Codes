using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingLevelConstructor : MonoBehaviour
{
    public bool showDebug;

    [Range(1,3)]
    public int debugLevel;

    public int[,] level1;
    public int[,] level2;
    public int[,] level3;

    //public int width = 35;
    //public int height = 45;

    //public float gridPosX;
    //public float gridPosY;

    public GameObject[] environmentPrefabs;

    /*
    0 = Floor
    1 = Wall
    2 = Door
    3 = Secret Door
    4 = Statue
    5 = Stairs Down
    6 = Stairs Up
    7 = Secret Door Lever (for opening secret doors)
    8 = Lever (for opening doors)

    */
    private void Awake()
    {
        level1 = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,0,1,1,1,1,1,1,1 },
            {1,1,1,1,1,0,1,1,1,1,1,1,1 },
            {1,1,1,1,1,3,1,1,1,1,1,1,1 },
            {1,1,1,0,0,0,0,0,0,0,1,1,1 },
            {1,1,1,0,0,0,0,0,0,0,0,1,1 },
            {1,1,8,0,0,0,0,0,0,0,1,8,1 },
            {1,5,2,0,0,4,0,0,0,0,2,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,0,1 },
            {1,1,1,1,0,0,0,0,1,1,1,0,1 },
            {1,1,1,1,0,0,0,0,1,1,1,0,1 },
            {1,1,1,1,0,0,0,0,1,1,1,0,1 },
            {1,1,1,1,0,0,0,0,2,0,0,0,1 },
            {1,1,1,1,0,0,0,0,8,1,1,1,1 },
            {1,1,1,1,2,1,1,1,1,1,1,1,1 },
            {1,1,1,1,0,8,1,1,1,1,1,1,1 },
            {1,1,1,1,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,0,1,1,1,1,1,1,1,1 },
            {1,1,1,1,2,8,1,1,1,1,1,1,1 },
            {1,1,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1 }
        };

        level2 = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,6,1,1,1,0,1,5,1,0,1,1,1,0,0,0,1,1,1 },
            {1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,0,1,0,1,0,1,1,1,1,1,0,1,1,1 },
            {1,1,1,1,1,1,1,1,1,0,2,0,0,0,0,0,1,1,1,1,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,0,1 },
            {1,1,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,2,0,0,0,0,0,1,1,1,0,1 },
            {1,1,1,0,0,0,1,0,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,2,1,1,1,1,1,2,1,0,3,0,1 },
            {1,1,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,1 },
            {1,1,1,1,1,3,1,2,1,0,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,1 },
            {1,1,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
            {1,1,1,0,0,0,0,0,1,0,1,0,1,0,1,1,1,0,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,1,1 },
            {1,1,1,0,0,0,0,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1 },
            {1,1,1,0,0,0,0,0,1,0,1,1,1,1,1,0,1,1,1,0,1,1,1,1,1,1,2,1,1,2,1,0,1,1,1 },
            {1,1,1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,0,0,1,1,0,1,1,0,1,0,0,0,1 },
            {1,0,0,0,0,0,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,0,0,1,4,0,4,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,1,0,1,1,1,0,3,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,0,1,1,1,1,1,0,0,0,1,1,0,1,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,1,1,1,1,1,0,1,1,1,1,1,0,0,0,1,4,0,4,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,1,1,0,1,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,1,1,1,0,1,1,0,1,1,1,1,1,0,0,0,1,1,0,1,1,0,0,0,1,0,1 },
            {1,1,1,1,1,1,1,2,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,4,0,4,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,3,0,1,1,0,1,0,0,1,0,0,0,0,0,0,0,1,1,0,1,1,0,0,0,1,0,1 },
            {1,0,0,0,0,0,0,0,1,1,1,1,0,1,1,1,1,0,0,0,0,0,0,0,1,1,0,1,1,2,1,3,1,0,1 },
            {1,0,0,0,1,0,0,0,3,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,4,0,4,1,0,1,0,1,0,1 },
            {1,0,0,1,1,1,0,0,1,1,1,0,1,1,1,0,1,1,1,0,0,0,0,0,1,1,0,1,1,0,1,1,1,0,1 },
            {1,0,0,0,1,0,0,0,3,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,1,0,1,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,1,0,1,0,0,0,1,0,1,4,0,4,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,3,0,0,0,1,0,1,1,0,1,1,0,1,1,1,0,1 },
            {1,2,1,1,3,1,1,1,1,2,1,1,1,3,1,1,1,1,1,1,1,2,1,2,1,1,1,1,1,2,1,1,1,0,1 },
            {1,0,1,1,0,1,1,0,0,0,0,1,1,0,0,0,2,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,1,1,0,0,0,0,1,1,3,1,1,1,0,1,1,1,1,1,0,0,1,0,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,1 },
            {1,1,1,3,1,0,1,1,1,0,0,0,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,2,1,1,1 },
            {1,1,0,0,1,0,1,1,1,0,0,0,3,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,1,0,1,0,0,0,1 },
            {1,1,0,0,1,2,1,1,1,1,1,1,1,0,0,0,1,2,1,1,1,1,1,1,1,0,0,0,1,0,1,0,0,0,1 },
            {1,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,3,0,0,0,1,0,1,0,0,0,1 },
            {1,1,1,1,1,0,0,0,0,0,0,0,2,0,0,0,1,1,1,1,1,0,1,3,1,0,0,0,1,0,1,0,0,0,1 },
            {1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,2,0,0,0,0,0,0,0,1,1,1,1,1,0,2,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };

        level3 = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,1,1,0,0,0,0,0,0,0,0,0,3,0,1,0,0,0,0,0,0,6,1 },
            {1,1,1,0,1,1,1,1,1,1,2,1,1,1,1,2,1,1,1,1,1,1,1 },
            {1,1,1,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
            {1,1,1,2,1,1,1,3,1,0,0,0,1,1,1,1,1,0,1,0,0,0,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,1,3,1,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,1,1,1,1,0,0,0,1 },
            {1,0,0,0,1,0,0,0,2,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,1,1,1,1,0,1,0,0,0,1 },
            {1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1 },
            {1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,2,1,1,1,1,2,1,1 },
            {1,2,1,1,1,0,0,2,0,0,2,0,0,2,0,0,0,0,0,0,0,0,1 },
            {1,0,0,0,1,2,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1 },
            {1,0,0,0,1,0,4,4,0,4,4,0,0,1,0,0,0,4,0,0,0,0,1 },
            {1,0,0,0,1,0,0,0,0,0,0,0,4,1,0,0,0,0,0,0,0,4,1 },
            {1,0,0,0,1,0,0,0,4,0,4,0,4,1,0,4,0,0,0,4,0,0,1 },
            {1,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,4,1 },
            {1,0,0,0,1,0,0,0,4,0,4,0,4,1,0,0,0,4,0,0,0,0,1 },
            {1,0,0,0,1,0,0,0,0,0,0,0,4,1,1,1,1,1,1,1,1,2,1 },
            {1,0,0,0,1,0,4,4,0,4,4,0,0,1,0,0,0,0,0,0,1,0,1 },
            {1,1,1,2,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,0,3,0,1 },
            {1,0,1,0,0,1,0,0,0,0,1,0,3,0,0,0,0,0,1,0,1,0,1 },
            {1,0,1,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,1 },
            {1,0,3,0,0,2,0,0,0,0,3,0,1,0,0,0,0,0,1,1,1,0,1 },
            {1,0,1,0,0,1,0,0,0,0,1,1,1,2,1,1,1,1,1,0,3,0,1 },
            {1,0,1,0,0,1,0,0,0,0,2,0,0,0,1,0,0,0,1,1,1,0,1 },
            {1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,1,0,1 },
            {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        };
    }

    private void Start()
    {
        level1 = LevelLayouts.level1;
        level2 = LevelLayouts.level2;
        level3 = LevelLayouts.level3;
        LevelGenerator();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            GenerateLevel1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            GenerateLevel2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            GenerateLevel3();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var x = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(x);
            Debug.Log($"Scene {SceneManager.GetActiveScene().buildIndex} reloaded.");
        }
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }
        
        int[,] maze;

        if (debugLevel == 1)
        {
            maze = level1;
        }
        else if (debugLevel == 2)
        {
            maze = level2;
        }
        else
        {
            maze = level3;
        }

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "...."; //empty
                }
                else if (maze[i, j] == 1)
                {
                    msg += "=="; //wall
                }
                else if (maze[i, j] == 2)
                {
                    msg += "DD"; //door
                }
                else if (maze[i, j] == 3)
                {
                    msg += "HD"; //hidden door
                }
                else if (maze[i, j] == 4)
                {
                    msg += "KK"; //statue
                }
                else if (maze[i, j] == 5)
                {
                    msg += "SS"; //stairs
                }
                else if (maze[i, j] == 6)
                {
                    msg += "CH"; //chain
                }
            }
            msg += "\n";
        }

        GUI.Label(new Rect(200, 200, 1500, 1500), msg);
    }

    public void GenerateLevel1()
    {
        int[,] maze = LevelLayouts.level1;

        int rows = maze.GetUpperBound(0);
        int cols = maze.GetUpperBound(1);

        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                //GameObject floor = environmentPrefabs[0];
                //Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                //floor.name = "Floor" + i + j;
                //floor.tag = "Floor";

                if (maze[i, j] == 0)
                {
                    GameObject g = environmentPrefabs[0];
                    Instantiate(g, new Vector3(j, -1, -i), Quaternion.identity);
                    g.name = "Floor i" + i + " j" + j;
                    g.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -1, -i), Quaternion.identity);

                }
                else if (maze[i, j] == 1)
                {
                    GameObject g = environmentPrefabs[1];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Wall i" + i + " j" + j;
                    g.tag = "Wall";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 2)
                {
                    GameObject g = environmentPrefabs[2];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Door i" + i + " j" + j;
                    g.tag = "Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 3)
                {
                    GameObject g = environmentPrefabs[3];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Secret_Door i" + i + " j" + j;
                    g.tag = "Secret Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 4)
                {
                    GameObject g = environmentPrefabs[4];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Statue i" + i + " j" + j;
                    g.tag = "Statue";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 5)
                {
                    GameObject g = environmentPrefabs[5];
                    Instantiate(g, new Vector3(j, -0.5f, -i), Quaternion.identity);
                    g.name = "StairsDown i" + i + " j" + j;
                    g.tag = "StairsDown";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 6)
                {
                    GameObject g = environmentPrefabs[6];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "StairsUp i" + i + " j" + j;
                    g.tag = "StairsUp";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 7)
                {
                    GameObject g = environmentPrefabs[7];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Secret_Lever i" + i + " j" + j;
                    g.tag = "Secret Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 8)
                {
                    GameObject g = environmentPrefabs[8];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Lever i" + i + " j" + j;
                    g.tag = "Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
            }
        }
    }

    public void GenerateLevel2()
    {
        int[,] maze = LevelLayouts.level2;

        int rows = maze.GetUpperBound(0);
        int cols = maze.GetUpperBound(1);

        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                //GameObject floor = environmentPrefabs[0];
                //Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                //floor.name = "Floor" + i + j;
                //floor.tag = "Floor";

                if (maze[i, j] == 0)
                {
                    GameObject g = environmentPrefabs[0];
                    Instantiate(g, new Vector3(j, -1, -i), Quaternion.identity);
                    g.name = "Floor i" + i + " j" + j;
                    g.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -1, -i), Quaternion.identity);

                }
                else if (maze[i, j] == 1)
                {
                    GameObject g = environmentPrefabs[1];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Wall i" + i + " j" + j;
                    g.tag = "Wall";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 2)
                {
                    GameObject g = environmentPrefabs[2];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Door i" + i + " j" + j;
                    g.tag = "Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 3)
                {
                    GameObject g = environmentPrefabs[3];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Secret_Door i" + i + " j" + j;
                    g.tag = "Secret Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 4)
                {
                    GameObject g = environmentPrefabs[4];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Statue i" + i + " j" + j;
                    g.tag = "Statue";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 5)
                {
                    GameObject g = environmentPrefabs[5];
                    Instantiate(g, new Vector3(j, -0.5f, -i), Quaternion.identity);
                    g.name = "StairsDown i" + i + " j" + j;
                    g.tag = "StairsDown";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 6)
                {
                    GameObject g = environmentPrefabs[6];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "StairsUp i" + i + " j" + j;
                    g.tag = "StairsUp";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 7)
                {
                    GameObject g = environmentPrefabs[7];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Secret_Lever i" + i + " j" + j;
                    g.tag = "Secret Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 8)
                {
                    GameObject g = environmentPrefabs[8];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Lever i" + i + " j" + j;
                    g.tag = "Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
            }
        }
    }

    public void GenerateLevel3()
    {
        int[,] maze = LevelLayouts.level3;

        int rows = maze.GetUpperBound(0);
        int cols = maze.GetUpperBound(1);

        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= cols; j++)
            {
                //GameObject floor = environmentPrefabs[0];
                //Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                //floor.name = "Floor" + i + j;
                //floor.tag = "Floor";

                if (maze[i, j] == 0)
                {
                    GameObject g = environmentPrefabs[0];
                    Instantiate(g, new Vector3(j, -1, -i), Quaternion.identity);
                    g.name = "Floor i" + i + " j" + j;
                    g.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -1, -i), Quaternion.identity);

                }
                else if (maze[i, j] == 1)
                {
                    GameObject g = environmentPrefabs[1];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Wall i" + i + " j" + j;
                    g.tag = "Wall";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 2)
                {
                    GameObject g = environmentPrefabs[2];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Door i" + i + " j" + j;
                    g.tag = "Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 3)
                {
                    GameObject g = environmentPrefabs[3];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Secret_Door i" + i + " j" + j;
                    g.tag = "Secret Door";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 4)
                {
                    GameObject g = environmentPrefabs[4];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Statue i" + i + " j" + j;
                    g.tag = "Statue";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule), new Vector3(j, 0, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 5)
                {
                    GameObject g = environmentPrefabs[5];
                    Instantiate(g, new Vector3(j, -0.5f, -i), Quaternion.identity);
                    g.name = "StairsDown i" + i + " j" + j;
                    g.tag = "StairsDown";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 6)
                {
                    GameObject g = environmentPrefabs[6];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "StairsUp i" + i + " j" + j;
                    g.tag = "StairsUp";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 7)
                {
                    GameObject g = environmentPrefabs[7];
                    Instantiate(g, new Vector3(j, 0, -i), Quaternion.identity);
                    g.name = "Secret_Lever i" + i + " j" + j;
                    g.tag = "Secret Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
                else if (maze[i, j] == 8)
                {
                    GameObject g = environmentPrefabs[8];
                    Instantiate(g, new Vector3(j, 0.5f, -i), Quaternion.identity);
                    g.name = "Lever i" + i + " j" + j;
                    g.tag = "Lever";

                    GameObject floor = environmentPrefabs[0];
                    Instantiate(floor, new Vector3(j, -1, -i), Quaternion.identity);
                    floor.name = "Floor" + i + j;
                    floor.tag = "Floor";

                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(j, -0.5f, -i), Quaternion.identity);
                }
            }
        }
    }

    private void LevelGenerator()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GenerateLevel1();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GenerateLevel2();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GenerateLevel3();
        }
    }
}
