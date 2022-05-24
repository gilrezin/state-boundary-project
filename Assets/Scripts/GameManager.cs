using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public GameObject SelectedPixels;
    private GameObject[] pixels;
    public DisplayMap displayMapScript;
    // Start is called before the first frame update
    void Start() {
        Screen.SetResolution(1077, 606, false);
    }

    // Update is called once per frame
    void Update() 
    {
        // get mouse position

        // when mouse position is down, search for the nearest pixels within a radius and color them in
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int positionX = toGridCoordinateX(cameraPosition.y);
            int positionY = toGridCoordinateY(cameraPosition.x);
            Debug.Log(positionX + ", " + positionY);
            if (positionX < 128 && positionY < 263)
            {
                for (int x = positionX - 2; x < positionX + 2; x++)
                {
                    for (int y = positionY - 2; y < positionY + 2; y++)
                    {
                        try 
                        {
                        World.world[x, y].IsSelected = true;
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
                        World.world[x, y].drewOn = true;
                        } catch {}
                    }
                }
            }
        }
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public void CalculateStability() 
    {
        //pixels = displayMapScript.pixelArray;
        //CompositeCollider2D country = SelectedPixels.AddComponent<CompositeCollider2D>();
        //country.attachedRigidbody.bodyType = RigidbodyType2D.Static;
        pixels = new GameObject[SelectedPixels.transform.childCount]; // get all selected pixels inside an array
        int index = 0;
        foreach (Transform child in SelectedPixels.transform)
        {
            pixels[index] = child.gameObject;
            index++;
        }
        
        //Debug.Log(pixels[0].name + " | " + pixels[1].name);

        // determine if borders are fractured - create an array of contiguous pixels
        List<GameObject> contiguousPixels = new List<GameObject>();
        // find the first pixel in the script, then find its neighbors
        contiguousPixels.Add(pixels[0]);
        // go to the next pixel in the list, then find its neighbors. If its neighbors are already in the list, then do not add them
        int adjustedXCoordinate;
        int adjustedYCoordinate;
        String coordinateIndex = "";
        for (int i = 0; i < contiguousPixels.Count; i++)
        {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            //int baseXCoordinate = toGridCoordinateX(contiguousPixels[i].transform.position.x);
            String pixelName = contiguousPixels[i].gameObject.name;
            int baseXCoordinate = int.Parse(pixelName.Substring(0, pixelName.IndexOf(", ")));
            //int baseYCoordinate = toGridCoordinateY(contiguousPixels[i].transform.position.y);
            int baseYCoordinate = int.Parse(pixelName.Substring(pixelName.IndexOf(", ") + 1));
            Debug.Log("Base Coordinate: " + baseXCoordinate + ", " + baseYCoordinate);
            //Debug.Log(contiguousPixels[i].name);
            try 
            {
                adjustedXCoordinate = baseXCoordinate - 1;
                if (!coordinateIndex.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate).name))
                //if (!contiguousPixels.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate)))
                    contiguousPixels.Add(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate));
                    Debug.Log("added " + adjustedXCoordinate + ", " + baseYCoordinate);
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (!coordinateIndex.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate).name))
                //if (!contiguousPixels.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate)))
                    contiguousPixels.Add(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate));
                    Debug.Log("added " + adjustedXCoordinate + ", " + baseYCoordinate);
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (!coordinateIndex.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate).name))
                //if (!contiguousPixels.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate)))
                    contiguousPixels.Add(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate));
                    Debug.Log("added " + baseXCoordinate + ", " + adjustedYCoordinate);
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedYCoordinate = baseYCoordinate + 1;
                if (!coordinateIndex.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate).name))
                //if (!contiguousPixels.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate)))
                    contiguousPixels.Add(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate));
                    Debug.Log("added " + baseXCoordinate + ", " + adjustedYCoordinate);
            } catch {Debug.Log("caught");}
            coordinateIndex = "";
            foreach(GameObject g in contiguousPixels)
            {
                try {
                coordinateIndex += g.name + " | ";
                } catch {/*Debug.Log("ran into an error. output until that point:\n" + coordinateIndex)*/;}
            }
            Debug.Log(coordinateIndex);
        }
        if (pixels.Length == contiguousPixels.Count)
        {
            Debug.Log("not fractured");
        } else { Debug.Log("fractured"); }
    }

    private int toGridCoordinateX(float input)
    {
        return (int) (Mathf.Abs((input - 4.9f) * 15)); 
    }

    private int toGridCoordinateY(float input)
    {
        return (int) (Mathf.Abs((input + 8.733333f) * 15));
    }
}
