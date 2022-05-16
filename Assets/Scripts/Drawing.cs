using System;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public int xSize = 86;
    public int ySize = 176;
    public int numberOfCenters = 20;
     void Start()
    {

        int smoothSquareLength = new System.Random().Next(7, 52);

        int minimumCentreX = (int)Math.Round(xSize * -0.1);
        int maximumCentreX = (int)Math.Round(xSize * 1.1);

        int minimumCentreY = (int)Math.Round(xSize * -0.1);
        int maximumCentreY = (int)Math.Round(xSize * 1.1);

        int[,] centers = new int[numberOfCenters, 2];

        for (int i = 0; i < numberOfCenters; i++)
        {
            centers[i, 0] = randint(maximumCentreX);
            centers[i, 1] = randint(maximumCentreY);
        }



        double valueLimitPercentage = 100d;
        double[,] data = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                List<Double> list = new List<Double>();
                for (int i = 0; i < (centers.Length)/2; i++)
                {

                    list.Add(Math.Sqrt(Math.Pow(centers[i, 0] - 1 - x, 2) + Math.Pow(centers[i, 1] - 1 - y, 2)));
                }
                list.Sort();
                double weightedAverageDistanceFromCenter = list[0];
                data[x, y] = (randdouble(1) * (weightedAverageDistanceFromCenter / 2));
            }
        }




        double total = 0;
        int count = xSize * ySize;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                total = total + data[x, y];
            }
        }




        double[,] averagedData = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                double a = 0;
                int c = 0;
                for (int xOffset = smoothSquareLength * -1; xOffset <= smoothSquareLength; xOffset++)
                {
                    for (int yOffset = smoothSquareLength * -1; yOffset <= smoothSquareLength; yOffset++)
                    {
                        if (x + xOffset <= (xSize - 1) && y + yOffset <= (ySize - 1) && x + xOffset >= 0 && y + yOffset >= 0)
                        {
                            a += data[x + xOffset, y + yOffset];
                            c++;
                        }
                    }
                }
                averagedData[x, y] = a / c;
            }
        }



        double average = total / count;
        // System.out.println(total);
        // System.out.println(count);
        // System.out.println(average);
        Debug.Log("Number of centers: " + numberOfCenters);
        Debug.Log("Smooth Radius Check: " + smoothSquareLength);
        // Step 1: Create our heat map chart using our data.

        double[,] smoothData = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (averagedData[x, y] > (average * (valueLimitPercentage / 100)))
                {
                    smoothData[x, y] = 0;

                }
                else smoothData[x, y] = 1;
            }
        }


        string str = "";
        Debug.Log(xSize + " " + ySize);
        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (smoothData[x, y] == 1)
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
