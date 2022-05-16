using System;
using System.Collections.Generic;
using TestForHugProject;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public Pixel[,] world;
    public double scaleFactor = 3;
    public int minimumNumberOfCircles = 10;
    public int maximumNumberOfCircles = 40;
    void Start()
    {
        int xSize = 86;
        int ySize = 176;
        
        world = App.GenerateWorld((int) (xSize*scaleFactor), (int) (ySize*scaleFactor), minimumNumberOfCircles, maximumNumberOfCircles);


        Debug.Log(xSize + " " + ySize);
        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize * scaleFactor; x++)
        {
            Debug.Log("test");

            for (int y = 0; y < ySize * scaleFactor; y++)
            {
                Debug.Log("test");
                if (world[x, y].ElevationPercentage >= 0)
                {
                    Debug.Log(world[x, y].ElevationPercentage + " ");
                    transform.position = new Vector3(transform.position.x + 0.1f / (float) scaleFactor, transform.position.y, 0);

                }
                else 
                {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float) scaleFactor, transform.position.y, 0);
                    instantiatedPixel = (GameObject) Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.name = y + " " + x;
                }
            }

            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f / (float) scaleFactor, 0);
            //Debug.Log("new line " + x);
        }
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
