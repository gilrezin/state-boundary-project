using System.Collections;
using System.Collections.Generic;
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
            int positionX = (int) (Mathf.Abs((cameraPosition.x + 8.733333f) * 15));
            int positionY = (int) (Mathf.Abs((cameraPosition.y - 4.9f) * 15));
            Debug.Log(positionX + ", " + positionY);
            for (int x = positionX - 2; x < positionX + 2; x++)
            {
                for (int y = positionY - 2; y < positionY + 2; y++)
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
        pixels = new GameObject[SelectedPixels.transform.childCount]; // get all pixels inside an array
        int index = 0;
        foreach (Transform child in SelectedPixels.transform)
        {
            pixels[index] = child.gameObject;
            index++;
        }
        
        //Debug.Log(pixels[0].name + " | " + pixels[1].name);

        // determine if borders are fractured - create an array of contiguous pixels
        ArrayList contiguousPixels = new ArrayList();
        // find the first pixel in the script, then find its neighbors
        contiguousPixels.Add(pixels[0]);
        // go to the next pixel in the list, then find its neighbors. If its neighbors are already in the list, then do not add them
        int adjustedXCoordinate;
        int adjustedYCoordinate;
        for (int i = 0; i < contiguousPixels.Count - 1; i++)
        {
            // search for this pixel's immediate neighbors. Add them to the list if they are not a duplicate
            int baseXCoordinate = pixels[i].GetComponent<PixelBehavior>().x;
            int baseYCoordinate = pixels[i].GetComponent<PixelBehavior>().y;
            try 
            {
                adjustedXCoordinate = baseXCoordinate - 1;
                if (!contiguousPixels.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate)))
                    contiguousPixels.Add(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate));
                    Debug.Log("added");
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedXCoordinate = baseXCoordinate + 1;
                if (!contiguousPixels.Contains(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate)));
                    contiguousPixels.Add(GameObject.Find(adjustedXCoordinate + ", " + baseYCoordinate));
                    Debug.Log("added");
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedYCoordinate = baseYCoordinate - 1;
                if (!contiguousPixels.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate)));
                    contiguousPixels.Add(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate));
                    Debug.Log("added");
            } catch {Debug.Log("caught");}
            try 
            {
                adjustedYCoordinate = baseXCoordinate + 1;
                if (!contiguousPixels.Contains(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate)));
                    contiguousPixels.Add(GameObject.Find(baseXCoordinate + ", " + adjustedYCoordinate));
                    Debug.Log("added");
            } catch {Debug.Log("caught");}
        }
        if (pixels.Length == contiguousPixels.Count)
        {
            Debug.Log("not fractured");
        } else { Debug.Log("fractured"); }
    }
}
