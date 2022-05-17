using System;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public Pixel[,] world;
    public double scaleFactor = 3;
    public int minimumNumberOfCircles = 70;
    public int maximumNumberOfCircles = 87;
    void Start() {
        int xSize = 86;
        int ySize = 176;

        world = WorldGenerator.GenerateWorld((int)(xSize * scaleFactor), (int)(ySize * scaleFactor), minimumNumberOfCircles, maximumNumberOfCircles);

        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize * scaleFactor; x++) {

            for (int y = 0; y < ySize * scaleFactor; y++) {
                if (world[x, y].ElevationPercentage >= 0) {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);

                }
                else {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    instantiatedPixel = (GameObject)Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.name = y + ", " + x;
                }
            }

            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f / (float)scaleFactor, 0);
        }
        return;
    }
    public static double randdouble(double max) {
        System.Random random = new System.Random();
        return (max + .0000000001) * random.NextDouble();
    }
    public static int randint(int max) {
        System.Random random = new System.Random();
        return random.Next(max + 1);
    }

    static bool noDropRNG(double percentChance, int rolls) {
        for (int x = rolls; x != 0; x--) {
            System.Random random = new System.Random();

            double randomNum = random.NextDouble();// Get a System.Random  value from 0.00 - 1.00
            double adjustedPercentChance = percentChance;

            if (randomNum <= adjustedPercentChance / 100) {
                return true;
            }
        }
        return false;
    }
}
