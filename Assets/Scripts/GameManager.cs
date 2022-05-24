using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public GameObject SelectedPixels;
    private string[] pixels;
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
            if (positionX < 128 && positionY < 263) {
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

                            GameObject.Find(x + ", " + y).transform.parent = GameObject.Find("SelectedPixels").transform; // selected pixel becomes a child of the SelectedPixels parent
                        }
                        catch { }
                    }
                }
            }
        }
    }

    public void CalculateStability() {
        pixels = new string[SelectedPixels.transform.childCount]; // get all selected pixels inside an array
        int index = 0;
        foreach (Transform child in SelectedPixels.transform) {
            pixels[index] = child.gameObject.name;
            index++;
        }


        // determine if borders are fractured - create an array of contiguous pixels
        List<String> contiguousPixels = new() {
            // find the first pixel in the script, then find its neighbors
            pixels[0]
        };
        // go to the next pixel in the list, then find its neighbors. If its neighbors are already in the list, then do not add them
        int adjustedXCoordinate;
        int adjustedYCoordinate;
        for (int i = 0; i < contiguousPixels.Count; i++) {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            String pixelName = contiguousPixels[i];
            int baseXCoordinate = int.Parse(pixelName[..pixelName.IndexOf(", ")]);
            int baseYCoordinate = int.Parse(pixelName[(pixelName.IndexOf(", ") + 1)..]);
            try {
                adjustedXCoordinate = baseXCoordinate - 1;
                if (!contiguousPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    if (Array.IndexOf(pixels, adjustedXCoordinate + ", " + baseYCoordinate) != -1)
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
            }
            catch {}
            try {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (!contiguousPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    if (Array.IndexOf(pixels, adjustedXCoordinate + ", " + baseYCoordinate) != -1) {
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
                    }
            }
            catch {}
            try {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (Array.IndexOf(pixels, baseXCoordinate + ", " + adjustedYCoordinate) != -1)
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch {}
            try {
                adjustedYCoordinate = baseYCoordinate + 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (Array.IndexOf(pixels, baseXCoordinate + ", " + adjustedYCoordinate) != -1)
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch {}
        }
        if (pixels.Length == contiguousPixels.Count) {
            Debug.Log(contiguousPixels.Count);
            Debug.Log("not fractured");
            Debug.Log(contiguousPixels.Count / pixels.Length);
        }
        else {
            Debug.Log(contiguousPixels.Count);
            Debug.Log((double) ((double)contiguousPixels.Count) / ((double)pixels.Length));
            Debug.Log("fractured");
        }
    }

    private int ToGridCoordinateX(float input) {
        return (int)(Mathf.Abs((input - 4.9f) * 15));
    }

    private int ToGridCoordinateY(float input) {
        return (int)(Mathf.Abs((input + 8.733333f) * 15));
    }

    private bool isElongated() // chccks to see if state shape is elongated
    {
        int smallestX = 0;
        int largestX = 0;
        int smallestY = 0;
        int largestY = 0;
        foreach(GameObject g in pixels) // runs through every pixel for their x and y values to find the mins and maxes
        {
            int xCoordinate = int.Parse(g.name.Substring(0, g.name.IndexOf(", ")));
            int yCoordinate = int.Parse(g.name.Substring(g.name.IndexOf(", ") + 1));
            if (xCoordinate > largestX)
                largestX = xCoordinate;
            if (xCoordinate < smallestX)
                smallestX = xCoordinate;
            if (yCoordinate > largestY)
                largestY = yCoordinate;
            if (yCoordinate < smallestY)
                smallestY = yCoordinate;
        }
        int xDifference = largestX - smallestX;
        int yDifference = largestY - smallestY;
        if (xDifference * 2 > yDifference || xDifference < yDifference * 2)
            return true;
        else
            return false;
    }
}
