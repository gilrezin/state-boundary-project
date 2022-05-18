using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour {
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public GameObject[,] pixelArray;
    public GameObject loadingText;
    public Pixel[,] world;
    public double scaleFactor = 1.5;
    public int minimumNumberOfCircles = 35;
    public int maximumNumberOfCircles = 44;

    public int xSize = 86;
    public int ySize = 176;
    void Start() {
        
        loadingText.SetActive(true);
        world = WorldGenerator.GenerateWorld((int)(xSize * scaleFactor), (int)(ySize * scaleFactor), minimumNumberOfCircles, maximumNumberOfCircles);
        pixelArray = new GameObject[(int)(xSize * scaleFactor), (int)(ySize* scaleFactor)];

        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize * scaleFactor; x++) {

            for (int y = 0; y < ySize * scaleFactor; y++) {
                Pixel pixelData = world[x, y];
                if (pixelData.ElevationPercentage <= 0) {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    pixelArray[x, y] = null;

                }
                else {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    instantiatedPixel = (GameObject)Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
                    instantiatedPixel.AddComponent<PixelBehavior>();
                    instantiatedPixel.name = y + ", " + x;

                    pixelArray[x, y] = instantiatedPixel;
                }
            }

            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f / (float)scaleFactor, 0);
        }
        loadingText.SetActive(false);
        return;
    }
    
    public void displayEthnicityColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x,y];
                Pixel pixelData = world[x, y];
                if (pixel == null) continue;

                pixel.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    public void displayLandColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = world[x, y];
                if (pixel == null)
                    continue;
                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
            }
        }
        foreach(Transform child in GameObject.Find("SelectedPixels").transform) // goes through every pixel that has been selected by the user and recolors it red
        {
            child.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void displayOilColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetOilColor();
            }
        }
    }

    public void displayGoldColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetGoldColor();
            }
        }
    }

    public void displayWoodColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetWoodColor();
            }
        }
    }
}
