using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour {
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public Pixel[,] world;
    public double scaleFactor = 1.5;
    public int minimumNumberOfCircles = 35;
    public int maximumNumberOfCircles = 44;
    void Start() {
        int xSize = 86;
        int ySize = 176;

        world = WorldGenerator.GenerateWorld((int)(xSize * scaleFactor), (int)(ySize * scaleFactor), minimumNumberOfCircles, maximumNumberOfCircles);

        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize * scaleFactor; x++) {

            for (int y = 0; y < ySize * scaleFactor; y++) {
                Pixel pixelData = world[x, y];
                if (pixelData.ElevationPercentage <= 0) {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);

                }
                else {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    instantiatedPixel = (GameObject)Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
                    instantiatedPixel.name = y + ", " + x;
                    
                }
            }

            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f / (float)scaleFactor, 0);
        }
        return;
    }
}
