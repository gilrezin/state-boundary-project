using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehavior : MonoBehaviour
{
    public int x;
    public int y;
    public DisplayMap displayMapScript;
    public bool drewOn = false;
    SpriteRenderer m_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        // gets the pixel's coordinates in the world, then looks up its values from DisplayMap
        /*String name = gameObject.name;
        int xCoordinateLength = name.indexOf(" ")
        int xCoordinate = name.Substring(0, xCoordinateLength);
        int yCoordinate = name.Substring(xCoordinateLength);*/
        displayMapScript = GameObject.Find("GameManager").GetComponent<DisplayMap>();
        //displayMapScript.world[]
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks to see if mouse is near, then if it is, turn to red when left click is down. Allows for drawing larger sizes
        Vector2 changeInCoords = new Vector2(Mathf.Abs(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Abs(transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
        if (Mathf.Sqrt(Mathf.Pow(changeInCoords.x,2) + Mathf.Pow(changeInCoords.y,2)) < 0.25f)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                World.world[x, y].IsSelected = true;
                if (World.currentView.Equals("LAND"))
                    m_Renderer.color = World.world[x, y].GetLandColor();
                if (World.currentView.Equals("NATION"))
                    m_Renderer.color = World.world[x, y].GetEthnicityColor();
                if (World.currentView.Equals("WOOD"))
                    m_Renderer.color = World.world[x, y].GetWoodColor();
                if (World.currentView.Equals("OIL"))
                    m_Renderer.color = World.world[x, y].GetOilColor();
                if (World.currentView.Equals("GOLD"))
                    m_Renderer.color = World.world[x, y].GetGoldColor();

                gameObject.transform.parent = GameObject.Find("SelectedPixels").transform; // selected pixel becomes a child of the SelectedPixels parent
                drewOn = true;
            }
        }
    }
}
