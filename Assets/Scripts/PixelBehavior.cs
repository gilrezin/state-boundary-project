using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBehavior : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.Mouse0) && changeInCoords.x < 0.25f && changeInCoords.y < 0.25f)
        {
            m_Renderer.color = Color.red;
            gameObject.transform.parent = GameObject.Find("SelectedPixels").transform; // selected pixel becomes a child of the SelectedPixels parent
            drewOn = true;
        }
        /*else if (displayMapScript.settingBackToDefaultLayer && drewOn) // if display is switching back to land mode, switch back to highlighted pixels
        {
            m_Renderer.color = Color.red;
        }*/
    }
}
