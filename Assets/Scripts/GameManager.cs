
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject SelectedPixels;
    private List<string> pixels;
    private List<string> borders;
    public DisplayMap displayMapScript;
    public GameObject stabilityGUIBackground;
    public GameObject stabilityGUI;
    public TextMeshProUGUI header;
    public TextMeshProUGUI body;
    public TextMeshProUGUI footer;
    private List<String> bodyTexts = new();
    private int stabilityGUIPageNumber = 0;
    // Start is called before the first frame update
    void Start() {
        Screen.SetResolution(1077, 606, false);
    }

    // Update is called once per frame
    void Update() {
        // get mouse position

        // when mouse position is down, search for the nearest pixels within a radius and color them in
        if (stabilityGUIBackground.activeSelf == false)
        {
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
        } else
        {
             if (Input.GetKeyDown(KeyCode.Space)) // go to next page in stability explanation
             {
                 try
                 {
                    stabilityGUIPageNumber++;
                    body.text = bodyTexts[stabilityGUIPageNumber]; // change text
                     if (bodyTexts.Count == stabilityGUIPageNumber + 1) // changes footer text depending if on the last slide
                        footer.text = "Press space to exit (" + (stabilityGUIPageNumber + 1) + "/" + (bodyTexts.Count) + ")";
                    else
                        footer.text = "Press space to continue (" + (stabilityGUIPageNumber + 1) + "/" + (bodyTexts.Count) + ")";
                    
                 } catch // close menu once reached end
                 {
                    stabilityGUIBackground.SetActive(false);
                    stabilityGUI.SetActive(false);
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
        Debug.Log("Protruded: " + IsProtruded());
        int stability = 100;
        DisplayStabilityGUI(stability);
    }





    public void GetSelectedPixels() {
        pixels = new();
        foreach (Pixel pixel in World.world) {
            if (pixel == null || !pixel.drewOn)
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


            adjustedXCoordinate = baseXCoordinate - 1;

            try {

                if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
                else if (World.world[adjustedXCoordinate, baseYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }


            adjustedXCoordinate = baseXCoordinate + 1;
            try {

                if (pixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
                else if (World.world[adjustedXCoordinate, baseYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }


            adjustedYCoordinate = baseYCoordinate - 1;
            try {

                if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
                else if (World.world[baseXCoordinate, adjustedYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }

            adjustedYCoordinate = baseYCoordinate + 1;

            try {

                if (pixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
                else if (World.world[baseXCoordinate, adjustedYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }

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
            //Debug.Log("is not fractured");
            return contiguousPixels.Count / pixels.Count;
        }
        else {
            //Debug.Log("is fractured");
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
        int xDifference = largestX - smallestX; // get the differences in x & y values
        int yDifference = largestY - smallestY;
        //Debug.Log("xDifference: " + xDifference + "\nyDifference: " + yDifference); // debug
        if (xDifference * 2 < yDifference || xDifference > yDifference * 2) // if either the x or y-values are more than twice the value of the other value, the state is elongated
            return true;
        else
            return false;
    }


    public double IsProtruded()
    {
        // make Dictionaries that contain x-values and y-values, and the associated number of times they appear
        Dictionary<int, int> yValueChart = new Dictionary<int, int>();
        Dictionary<int, int> xValueChart = new Dictionary<int, int>();
        foreach (string pixel in pixels) { // for every pixel, obtain its y and x-values
            int xCoordinate = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int yCoordinate = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (!yValueChart.ContainsKey(yCoordinate)) // check if the y-value exists in the dictionary. If it doesn't add it
            {
                yValueChart.Add(yCoordinate, 1);
            } else // if it does exist, append it by 1
            {
                yValueChart[yCoordinate]++;
            }

            if (!xValueChart.ContainsKey(xCoordinate)) // check if the x-value exists in the dictionary. If it doesn't add it
            {
                xValueChart.Add(xCoordinate, 1);
            } else // if it does exist, append it by 1
            {
                xValueChart[xCoordinate]++;
            }
        }
        // find the average y-value
        double averageYValue = yValueChart.Values.Average();
        int abnormalYValues = 0;

        // find the average x-value
        double averageXValue = xValueChart.Values.Average();
        int abnormalXValues = 0;

        foreach(KeyValuePair<int, int> entry in yValueChart) // find the number of below average y-value occurences by row (panhandles)
        {
            if (entry.Value < averageYValue / 1.5)
            {
                abnormalYValues++;
            }
        }
        //Debug.Log("Abnormal y-values: " + abnormalYValues + "\ty-values: " + yValueChart.Count + "\tFraction: " + (double) abnormalYValues / yValueChart.Count); // debug

        foreach(KeyValuePair<int, int> entry in xValueChart) // find the number of below average y-value occurences by row (panhandles)
        {
            if (entry.Value < averageXValue / 1.5)
            {
                abnormalXValues++;
            }
        }
        //Debug.Log("Abnormal x-values: " + abnormalXValues + "\tx-values: " + xValueChart.Count + "\tFraction: " + (double) abnormalXValues / xValueChart.Count); // debug
        
        double protrusionCoefficient = Mathf.Max((float) abnormalYValues / yValueChart.Count, (float) abnormalXValues / xValueChart.Count); // determine the location of the panhandle, whether north-south or east-west
        if (protrusionCoefficient > 0.25) // if more than 25% of the values are considered "abnormal"
            return protrusionCoefficient; // the more protruded the state is, the higher the coefficient
        else { return 0; } // returns 0 if not protruded
    }

    private int ToGridCoordinateX(float input) {
        return (int)(Mathf.Abs((input - 4.9f) * 15));
    }

    private int ToGridCoordinateY(float input) {
        return (int)(Mathf.Abs((input + 8.733333f) * 15));
    }

    public void DisplayStabilityGUI(int stability) // displays the stability GUI and updates the text
    {
        assignBodyText();
        stabilityGUIPageNumber = 0;
        header.text = "This country is " + stability + "% stable.";
        try
        {
            body.text = bodyTexts[0];
            if (bodyTexts.Count == 1)
                footer.text = "Press space to exit (1/1)";
            else
                footer.text = "Press space to continue (1/" + (bodyTexts.Count) + ")";
        } catch 
        {
            body.text = "no problems found! :)";
            footer.text = "Press space to exit";
        }
        stabilityGUIBackground.SetActive(true);
        stabilityGUI.SetActive(true);
    }

    public void assignBodyText()
    {
        bodyTexts.Clear();
        if (IsFractured() < 1) // check if state is fractured
            bodyTexts.Add("Fragmented states create isolation from the mainland, sometimes leading to autonomy or devolution. Also, it is more difficult to evenly spread resources to all parts of the state. Isolation and differences add up as centrifugal forces and work to divide the states.");
        if (IsElongated()) // check if state is elongated
            bodyTexts.Add("Elongated states will separate one side from another creating isolation of the two groups. Additionally from a military perspective, long state borders are exposed more and require military enforcement across borders. Isolation and differences add up as centrifugal forces and work to divide the states.");
        /*if (IsProtruded > 0.25) // check if state is protruded
            bodyTexts.Add()*/
        /*if (OilBoundry > 0 || GoldBoundry > 0 || WoodBoundry > 0) // check if state has a resource crossing a border
            bodyTexts.Add("");*/
    }
}
