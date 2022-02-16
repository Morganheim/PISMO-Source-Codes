using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConstructor : MonoBehaviour
{
    public GameObject[] environmentPrefabs;

    private void Start()
    {
        LevelGenerator();
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
