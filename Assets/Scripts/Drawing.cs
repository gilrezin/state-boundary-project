using System;
using System.Collections.Generic;
using TestForHugProject;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public int xSize = 86;
    public int ySize = 176;
    public int numberOfCenters = 20;
    public Pixel[,] world;
     void Start()
    {

        world = App.GenerateWorld(xSize, ySize, 20, 60);


        string str = "";
        Debug.Log(xSize + " " + ySize);
        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (world[x, y].ElevationPercentage < 0)
                {
                    str += " 0 ";
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);

                }
                else 
                {
                    str += " - ";
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
                    instantiatedPixel = (GameObject) Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.name = y + " " + x;
                    
                }
            }
            str += "\n";
            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f, 0);
            //Debug.Log("new line " + x);
        }
        //Debug.Log(str);
        return;
    }
    public static double randdouble(double max)
    {
        System.Random random = new System.Random();
        return (max + .0000000001) * random.NextDouble();
    }
    public static int randint(int max)
    {
        System.Random random = new System.Random();
        return random.Next(max + 1);
    }

    static bool noDropRNG(double percentChance, int rolls)
    {
        for (int x = rolls; x != 0; x--)
        {
            System.Random random = new System.Random();

            double randomNum = random.NextDouble();// Get a System.Random  value from 0.00 - 1.00
            double adjustedPercentChance = percentChance;

            if (randomNum <= adjustedPercentChance / 100)
            {
                return true;
            }
        }
        return false;
    }
}
