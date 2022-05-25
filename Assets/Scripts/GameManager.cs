using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public GameObject SelectedPixels;
    private List<string> pixels;
    private List<string> borders;
    public DisplayMap displayMapScript;
    // Start is called before the first frame update
    void Start() {
        Screen.SetResolution(1077, 606, false);
    }

    // Update is called once per frame
    void Update() {
        // get mouse position

        // when mouse position is down, search for the nearest pixels within a radius and color them in
        if (Input.GetKey(KeyCode.Mouse0)) {
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int positionX = ToGridCoordinateX(cameraPosition.y);
            int positionY = ToGridCoordinateY(cameraPosition.x);
            for (int x = positionX - 2; x < positionX + 2; x++) {
                for (int y = positionY - 2; y < positionY + 2; y++) {
                    try {
                        World.world[x, y].drewOn = true;
                        if (World.currentView.Equals("LAND"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetLandColor();
                        else if (World.currentView.Equals("NATION"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetEthnicityColor();
                        else if (World.currentView.Equals("WOOD"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetWoodColor();
                        else if (World.currentView.Equals("OIL"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetOilColor();
                        else if (World.currentView.Equals("GOLD"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetGoldColor();
                    }
                    catch { }

                }
            }
        }

        else if (Input.GetKey(KeyCode.Mouse1)) {
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int positionX = ToGridCoordinateX(cameraPosition.y);
            int positionY = ToGridCoordinateY(cameraPosition.x);
            for (int x = positionX - 2; x < positionX + 2; x++) {
                for (int y = positionY - 2; y < positionY + 2; y++) {
                    try {
                        World.world[x, y].drewOn = false;
                        if (World.currentView.Equals("LAND"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetLandColor();
                        else if (World.currentView.Equals("NATION"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetEthnicityColor();
                        else if (World.currentView.Equals("WOOD"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetWoodColor();
                        else if (World.currentView.Equals("OIL"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetOilColor();
                        else if (World.currentView.Equals("GOLD"))
                            GameObject.Find(x + ", " + y).GetComponent<SpriteRenderer>().color = World.world[x, y].GetGoldColor();
                    }
                    catch { }

                }
            }
        }
    }




    public void CalculateStability() {
        GetSelectedPixels();
        displayMapScript.DisplayLandColor();

        FindBorders();
        Debug.Log("Fractured: " + IsFractured());
        Debug.Log("Elongated: " + IsElongated());
        Debug.Log("Oil: " + OilBoundry());
        Debug.Log("Wood: " + WoodBoundry());
        Debug.Log("Gold: " + GoldBoundry());
    }





    public void GetSelectedPixels() {
        pixels = new();
        foreach (Pixel pixel in World.world) {
            if (!pixel.drewOn)
                continue;
            pixels.Add(pixel.Coordinate[0] + ", " + pixel.Coordinate[1]);
        }
    }




    public void FindBorders() {
        borders = new();
        foreach (string pixel in pixels) {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            int adjustedXCoordinate;
            int adjustedYCoordinate;
            int baseXCoordinate = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int baseYCoordinate = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            int numOfNeighbors = 0;
            try {
                adjustedXCoordinate = baseXCoordinate - 1;
                if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
            }
            catch { }
            try {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate + 1;
                if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
            }
            catch { }
            if (numOfNeighbors != 4) {
                borders.Add(pixel);
                GameObject.Find(pixel).GetComponent<SpriteRenderer>().color = (World.world[baseXCoordinate, baseYCoordinate].GetLandColor() + Color.black) / 2;
            }

        }
    }





    public double OilBoundry() {
        if (borders.Count == 0)
            FindBorders();
        int borderOilCount = 0;
        int totalOilCount = 0;
        foreach (string pixel in borders) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasOil()) {
                borderOilCount++;
            }
        }
        foreach (string pixel in pixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasOil())
                totalOilCount++;
        }
        totalOilCount += borderOilCount;
        return (double)borderOilCount / (double)totalOilCount;
    }



    public double GoldBoundry() {
        if (borders.Count == 0)
            FindBorders();
        int borderGoldCount = 0;
        int totalGoldCount = 0;
        foreach (string pixel in borders) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasGold()) {
                borderGoldCount++;
            }
        }

        foreach (string pixel in pixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasGold())
                totalGoldCount++;
        }

        totalGoldCount += borderGoldCount;

        return (double)borderGoldCount / (double)totalGoldCount;


    }



    public double WoodBoundry() {
        if (borders.Count == 0)
            FindBorders();
        int borderWoodCount = 0;
        int totalWoodCount = 0;
        foreach (string pixel in borders) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasWood()) {
                borderWoodCount++;
            }
        }

        foreach (string pixel in pixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasWood())
                totalWoodCount++;
        }

        totalWoodCount += borderWoodCount;
        return (double)borderWoodCount / (double)totalWoodCount;
    }






    public double IsFractured() {
        // determine if borders are fractured - create an array of contiguous pixels
        List<string> contiguousPixels = new() {
            // find the first pixel in the script, then find its neighbors
            pixels[0]
        };
        // go to the next pixel in the list, then find its neighbors. If its neighbors are already in the list, then do not add them
        int adjustedXCoordinate;
        int adjustedYCoordinate;
        for (int i = 0; i < contiguousPixels.Count; i++) {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            string pixelName = contiguousPixels[i];
            int baseXCoordinate = int.Parse(pixelName[..pixelName.IndexOf(", ")]);
            int baseYCoordinate = int.Parse(pixelName[(pixelName.IndexOf(", ") + 1)..]);
            try {
                adjustedXCoordinate = baseXCoordinate - 1;
                if (!contiguousPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
            }
            catch { }
            try {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (!contiguousPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate)) {
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
                    }
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate + 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch { }
        }
        if (pixels.Count == contiguousPixels.Count) {
            return contiguousPixels.Count / pixels.Count;
        }
        else {
            return (double)((double)contiguousPixels.Count) / ((double)pixels.Count);
        }
    }







    public bool IsElongated() // chccks to see if state shape is elongated
    {
        int smallestX = int.MaxValue;
        int largestX = 0;
        int smallestY = int.MaxValue;
        int largestY = 0;
        foreach (string g in pixels) // runs through every pixel for their x and y values to find the mins and maxes
        {
            int xCoordinate = int.Parse(g[..g.IndexOf(", ")]);
            int yCoordinate = int.Parse(g[(g.IndexOf(", ") + 1)..]);
            if (xCoordinate > largestX) {
                largestX = xCoordinate;
            }
            else if (xCoordinate < smallestX) {
                smallestX = xCoordinate;
            }
            if (yCoordinate > largestY) {
                largestY = yCoordinate;
            }
            else if (yCoordinate < smallestY) {
                smallestY = yCoordinate;
            }
        }
        int xDifference = largestX - smallestX;
        int yDifference = largestY - smallestY;
        if (xDifference * 2 < yDifference || xDifference > yDifference * 2)
            return true;
        else
            return false;
    }

    private int ToGridCoordinateX(float input) {
        return (int)(Mathf.Abs((input - 4.9f) * 15));
    }

    private int ToGridCoordinateY(float input) {
        return (int)(Mathf.Abs((input + 8.733333f) * 15));
    }
}
