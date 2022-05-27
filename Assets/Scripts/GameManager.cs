
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private List<string> selectedPixels;
    private List<string> borders;
    public DisplayMap displayMapScript;
    public GameObject stabilityGUIBackground;
    public GameObject stabilityGUI;
    public TextMeshProUGUI header;
    public TextMeshProUGUI body;
    public TextMeshProUGUI footer;
    private List<string> bodyTexts = new();
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
        FindBorders();

        if (World.currentView.Equals("LAND"))
            displayMapScript.DisplayLandColor();
        else if (World.currentView.Equals("NATION"))
            displayMapScript.DisplayEthnicityColor();
        else if (World.currentView.Equals("WOOD"))
            displayMapScript.DisplayWoodColor();
        else if (World.currentView.Equals("OIL"))
            displayMapScript.DisplayOilColor();
        else if (World.currentView.Equals("GOLD"))
            displayMapScript.DisplayGoldColor();


        DisplayStabilityGUI(IsFractured(), IsElongated(), IsProtruded(), OilBoundry(), GoldBoundry(), WoodBoundry(), NumberOfEthnicities(), OilDensity(), GoldDensity(), WoodDensity());
    }




    public void DisplayStabilityGUI(double isFractured, bool isElongated, double isProtruded, double oilBoundry, double goldBoundry, double woodBoundry, int numberOfEthnicities, double oilDensity, double goldDensity, double woodDensity) // displays the stability GUI and updates the text
    {
        int numberOfFactors = 10;
        double stability = 0;
        stability += (1 - isFractured);
        stability += isProtruded;
        stability += oilBoundry;
        stability += goldBoundry;
        stability += woodBoundry;
        stability += oilDensity;
        stability += goldDensity;
        stability += woodDensity;
        stability += numberOfEthnicities * .2;
        if (isElongated) {
            stability += .75;
        }
        

        stability /= numberOfFactors;
        stability = 1 - stability;
        AssignBodyText(isFractured, isElongated, isProtruded, oilBoundry, goldBoundry, woodBoundry, numberOfEthnicities, oilDensity, goldDensity, woodDensity);
        stabilityGUIPageNumber = 0;
        header.text = "This country is " + Mathf.Round((float)(stability * 1000f)) / 10d + "% stable.";
        try {
            body.text = bodyTexts[0];
            if (bodyTexts.Count == 1)
                footer.text = "Press space to exit (1/1)";
            else
                footer.text = "Press space to continue (1/" + (bodyTexts.Count) + ")";
        }
        catch {
            body.text = "no problems found! :)";
            footer.text = "Press space to exit";
        }
        stabilityGUIBackground.SetActive(true);
        stabilityGUI.SetActive(true);
    }





    public void AssignBodyText(double isFractured, bool isElongated, double isProtruded, double oilBoundry, double goldBoundry, double woodBoundry, int numberOfEthnicities, double oilDensity, double goldDensity, double woodDensity) {
        bodyTexts.Clear();
        if (isFractured < .9) // check if state is fractured
            bodyTexts.Add("Fragmented states create isolation from the mainland, sometimes leading to autonomy or devolution. Also, it is more difficult to evenly spread resources to all parts of the state. Isolation and differences add up as centrifugal forces and work to divide the states. (" + Mathf.Round((float)((1d - isFractured) * 1000d)) / 10d + "% of your country is fragmented)");
        if (isElongated) // check if state is elongated
            bodyTexts.Add("Elongated states will separate one side from another creating isolation of the two groups. Additionally from a military perspective, long state borders are exposed more and require military enforcement across borders. Isolation and differences add up as centrifugal forces and work to divide the states.");
        if (isProtruded > 0.25) // check if state is protruded
            bodyTexts.Add("A protrusion can cause unnecessary disputes over its territory. Additionally smooth communication may be disturbed. This can isolate the mainland and the porruption. Isolation and differences add up as centrifugal forces and work to divide the states. (" + Mathf.Round((float)(isProtruded * 1000f)) / 10d + "% of you country is protruded)");
        if (oilBoundry > 0.1) // check if state has a resource crossing a border
            bodyTexts.Add("Oil crosses your state border. Valuable resources come with competition. States may have allocational boundary disputes to obtain as much of the resources. The constant redrawing and ruling of boundaries can create unnecessary frustration and paranoia. (" + Mathf.Round((float)(oilBoundry * 1000f)) / 10d + "% of your oil is shared)");
        if (oilDensity < .1)
            bodyTexts.Add("A lack of oil in your state can increase your dependance on other countries. States may have decreased barginning power, and increased reliance on the global market. (Only " + Mathf.Round((float)(oilDensity * 1000f)) / 10d + "% of your country has oil)");
        if (goldBoundry > 0.1) // check if state has a resource crossing a border
            bodyTexts.Add("Gold crosses your state border. Valuable resources come with competition. States may have allocational boundary disputes to obtain as much of the resources. The constant redrawing and ruling of boundaries can create unnecessary frustration and paranoia. (" + Mathf.Round((float)(goldBoundry * 1000f)) / 10d + "% of your gold is shared)");
        if (goldDensity < .1)
            bodyTexts.Add("A lack of gold in your state can increase your dependance on other countries. States may have decreased barginning power, and increased reliance on the global market. (Only " + Mathf.Round((float)(goldDensity * 1000f)) / 10d + "% of country has gold");
        if (woodBoundry > 0.1) // check if state has a resource crossing a border
            bodyTexts.Add("Wood crosses your state border. Valuable resources come with competition. States may have allocational boundary disputes to obtain as much of the resources. The constant redrawing and ruling of boundaries can create unnecessary frustration and paranoia. (" + Mathf.Round((float)(woodBoundry * 1000f)) / 10d + "% of your wood is shared)");
        if (woodDensity < .33)
            bodyTexts.Add("A lack of wood in your state can increase your dependance on other countries. States may have decreased barginning power, and increased reliance on the global market. (Only " + Mathf.Round((float)(woodDensity * 1000f)) / 10d + "% of your country has wood)");
        if (numberOfEthnicities > 2)
            bodyTexts.Add("More than 3 nationalities support the growth of centrifugal forces. The cultural differences create division and ideas of self determination in one of those groups can lead to war to be granted autonomy. Isolation and differences add up as centrifugal forces and work to divide the states. (There are " + numberOfEthnicities + " different nationalities in your country)");
    }




    public void GetSelectedPixels() {
        selectedPixels = new();
        foreach (Pixel pixel in World.world) {
            if (pixel == null)
                continue;
            if (!pixel.drewOn) {
                pixel.border = false;
                continue;
            }
            
            selectedPixels.Add(pixel.Coordinate[0] + ", " + pixel.Coordinate[1]);
        }
    }






    public void FindBorders() {
        borders = new();
        foreach (string pixel in selectedPixels) {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            int adjustedXCoordinate;
            int adjustedYCoordinate;
            int baseXCoordinate = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int baseYCoordinate = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            int numOfNeighbors = 0;


            adjustedXCoordinate = baseXCoordinate - 1;

            try {

                if (selectedPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
                else if (World.world[adjustedXCoordinate, baseYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }


            adjustedXCoordinate = baseXCoordinate + 1;
            try {

                if (selectedPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    numOfNeighbors++;
                else if (World.world[adjustedXCoordinate, baseYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }


            adjustedYCoordinate = baseYCoordinate - 1;
            try {

                if (selectedPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
                else if (World.world[baseXCoordinate, adjustedYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }

            adjustedYCoordinate = baseYCoordinate + 1;

            try {

                if (selectedPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    numOfNeighbors++;
                else if (World.world[baseXCoordinate, adjustedYCoordinate] == null) {
                    numOfNeighbors++;
                }
            }
            catch { numOfNeighbors++; }

            if (numOfNeighbors != 4) {
                borders.Add(pixel);
                World.world[baseXCoordinate, baseYCoordinate].border = true;
            }


        }

        if (World.currentView.Equals("LAND"))
            displayMapScript.DisplayLandColor();
        else if (World.currentView.Equals("NATION"))
            displayMapScript.DisplayEthnicityColor();
        else if (World.currentView.Equals("WOOD"))
            displayMapScript.DisplayWoodColor();
        else if (World.currentView.Equals("OIL"))
            displayMapScript.DisplayOilColor();
        else if (World.currentView.Equals("GOLD"))
            displayMapScript.DisplayGoldColor();
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
        foreach (string pixel in selectedPixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasOil())
                totalOilCount++;
        }
        totalOilCount += borderOilCount;
        if (((double)borderOilCount / (double)totalOilCount).Equals(double.NaN))
            return 0;
        return (double)borderOilCount / (double)totalOilCount;
    }

    public double GoldDensity() {
        int goldCount = 0;
        foreach (string pixel in selectedPixels) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasGold()) {
                goldCount++;
            }
        }
        if (((double)goldCount / (double)selectedPixels.Count).Equals(double.NaN))
            return 0;
        return (double) goldCount / (double) selectedPixels.Count;
    } 

    public double OilDensity() {
        int oilCount = 0;
        foreach (string pixel in selectedPixels) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasOil()) {
                oilCount++;
            }
        }

        if (((double)oilCount / (double)oilCount).Equals(double.NaN))
            return 0;
        return (double) oilCount / (double) selectedPixels.Count;

    }

    public double WoodDensity() {
        int woodCount = 0;
        foreach (string pixel in selectedPixels) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (World.world[x, y].HasWood()) {
                woodCount++;
            }
        }
        if (((double)woodCount / (double)woodCount).Equals(double.NaN))
            return 0;
        return (double) woodCount / (double) selectedPixels.Count;
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

        foreach (string pixel in selectedPixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasGold())
                totalGoldCount++;
        }

        totalGoldCount += borderGoldCount;
        if (((double)borderGoldCount / (double)totalGoldCount).Equals(double.NaN))
            return 0;
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

        foreach (string pixel in selectedPixels) {
            if (World.world[int.Parse(pixel[..pixel.IndexOf(", ")]), int.Parse(pixel[(pixel.IndexOf(", ") + 1)..])].HasWood())
                totalWoodCount++;
        }

        totalWoodCount += borderWoodCount;
        if (((double)borderWoodCount / (double)totalWoodCount).Equals(double.NaN))
            return 0;
        return (double)borderWoodCount / (double)totalWoodCount;
    }






    public double IsFractured() {
        // determine if borders are fractured - create an array of contiguous pixels
        List<string> contiguousPixels = new() {
            // find the first pixel in the script, then find its neighbors
            selectedPixels[0]
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
                    if (selectedPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
            }
            catch { }
            try {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (!contiguousPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate))
                    if (selectedPixels.Contains(adjustedXCoordinate + ", " + baseYCoordinate)) {
                        contiguousPixels.Add(adjustedXCoordinate + ", " + baseYCoordinate);
                    }
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (selectedPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch { }
            try {
                adjustedYCoordinate = baseYCoordinate + 1;
                if (!contiguousPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                    if (selectedPixels.Contains(baseXCoordinate + ", " + adjustedYCoordinate))
                        contiguousPixels.Add(baseXCoordinate + ", " + adjustedYCoordinate);
            }
            catch { }
        }
        if (selectedPixels.Count == contiguousPixels.Count) {
            return contiguousPixels.Count / selectedPixels.Count;
        }
        else {
            return (double)((double)contiguousPixels.Count) / ((double)selectedPixels.Count);
        }
    }







    public bool IsElongated() // chccks to see if state shape is elongated
    {
        int smallestX = int.MaxValue;
        int largestX = 0;
        int smallestY = int.MaxValue;
        int largestY = 0;
        foreach (string g in selectedPixels) // runs through every pixel for their x and y values to find the mins and maxes
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
        Dictionary<int, int> yValueChart = new();
        Dictionary<int, int> xValueChart = new();
        foreach (string pixel in selectedPixels) { // for every pixel, obtain its y and x-values
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
            return protrusionCoefficient; // the more protruded the state is, the higher the coefficient
    }




    public int NumberOfEthnicities() {
        List<string> ethincities = new();
        foreach (string pixel in selectedPixels) {
            int x = int.Parse(pixel[..pixel.IndexOf(", ")]);
            int y = int.Parse(pixel[(pixel.IndexOf(", ") + 1)..]);
            if (ethincities.Contains(World.world[x, y].EthinictyID)) {
                continue;
            }
            ethincities.Add(World.world[x, y].EthinictyID);
        }

        return ethincities.Count;
    }






    private int ToGridCoordinateX(float input) {
        return (int)(Mathf.Abs((input - 4.9f) * 15));
    }






    private int ToGridCoordinateY(float input) {
        return (int)(Mathf.Abs((input + 8.733333f) * 15));
    }

    
}
