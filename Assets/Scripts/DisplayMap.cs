using UnityEngine;

public class DisplayMap : MonoBehaviour {
    public GameObject pixel;
    public GameObject instantiatedPixel;
    public GameObject[,] pixelArray;
    public GameObject CurrentViewText;
    
    //public Pixel[,] world;
    public GameObject loadingText;
    public Pixel[,] world;
    public double scaleFactor = 1.5;
    public int minimumNumberOfCircles = 35;
    public int maximumNumberOfCircles = 44;

    public int xSize = 86;
    public int ySize = 176;
    void Start() {
        

        World.world = WorldGenerator.GenerateWorld((int)(xSize * scaleFactor), (int)(ySize * scaleFactor), minimumNumberOfCircles, maximumNumberOfCircles);
        loadingText.SetActive(true);
        pixelArray = new GameObject[(int)(xSize * scaleFactor), (int)(ySize* scaleFactor)];
        World.currentView = "LAND";

        transform.position = new Vector3(-8.8f, 4.9f, 0);
        for (int x = 0; x < xSize * scaleFactor; x++) {

            for (int y = 0; y < ySize * scaleFactor; y++) {
                Pixel pixelData = World.world[x, y];
                if (pixelData.ElevationPercentage <= 0) {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    pixelArray[x, y] = null;

                }
                else {
                    transform.position = new Vector3(transform.position.x + 0.1f / (float)scaleFactor, transform.position.y, 0);
                    instantiatedPixel = (GameObject)Instantiate(pixel, transform.position, Quaternion.identity);
                    instantiatedPixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
                    //instantiatedPixel.AddComponent<PixelBehavior>();
                    //instantiatedPixel.GetComponent<PixelBehavior>().x = x;
                    //instantiatedPixel.GetComponent<PixelBehavior>().y = y;
                    instantiatedPixel.name = x + ", " + y;

                    pixelArray[x, y] = instantiatedPixel;
                }
            }

            transform.position = new Vector3(-8.8f, transform.position.y - 0.1f / (float)scaleFactor, 0);
        }
        loadingText.SetActive(false);
        return;
    }
    
    public void DisplayEthnicityColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x,y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null) continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetEthnicityColor();
            }
        }
        World.currentView = "NATION";
    }

    public void DisplayLandColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null)
                    continue;
                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
            }
        }
        World.currentView = "LAND";

    }

    public void DisplayOilColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetOilColor();
            }
        }
        World.currentView = "OIL";
    }

    public void DisplayGoldColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetGoldColor();
            }
        }
        World.currentView = "GOLD";
    }

    public void DisplayWoodColor() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null)
                    continue;

                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetWoodColor();
            }
        }
        World.currentView = "WOOD";
    }

    public void ClearSelection() {
        for (int x = 0; x < xSize * scaleFactor; x++) {
            for (int y = 0; y < ySize * scaleFactor; y++) {
                GameObject pixel = pixelArray[x, y];
                Pixel pixelData = World.world[x, y];
                if (pixel == null)
                    continue;
                pixelData.drewOn = false;
                pixel.GetComponent<SpriteRenderer>().color = pixelData.GetLandColor();
            }
        }
        World.currentView = "LAND";
    }
}
