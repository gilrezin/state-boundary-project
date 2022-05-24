using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class  Pixel {
    public double ElevationPercentage;

    public int[] Coordinate = new int[2];

    // Resources
    public double OilResourcePercentage;
    public double GoldResorucePercentage = 0;
    public double WoodResorucePercentage = 0;
    public bool drewOn = false;

    public string EthinictyID = "";
    public Pixel(double ElevationPercentage, double OilResourcePercentage, double GoldResourcePercentage, double WoodResourcePercentage, string EthnicityID, int[] Coordinate) {
        this.ElevationPercentage = ElevationPercentage;
        this.OilResourcePercentage = OilResourcePercentage;
        this.GoldResorucePercentage = GoldResourcePercentage;
        this.WoodResorucePercentage = WoodResourcePercentage;

        this.EthinictyID = EthnicityID;

        this.Coordinate = Coordinate;

    }

    public void ToggleSelect() {
        if (drewOn)
            drewOn = false;
        else
            drewOn = true;
    }

    public Color GetLandColor() {
        Color color;
                
        if (ElevationPercentage < .2)
            color = new Color32(116, 183, 49, 255);
        else if (.2 <= ElevationPercentage && ElevationPercentage < .4)
            color = new Color32(78, 122, 36, 255);
        else if (.4 <= ElevationPercentage && ElevationPercentage < .6)
            color = new Color32(96, 186, 100, 255);
        else if (.6 <= ElevationPercentage && ElevationPercentage < .8)
            color = new Color32(96, 122, 100, 255);
        else
            color = new Color32(145, 187, 150, 255);

        if (drewOn == true)
            return (color + GetSelectedColor()) / 2;
        else
            return color;
    }

    public Color GetOilColor() {
        Color color;
        if (OilResourcePercentage < .2)
            color = Color.white;
        else if (.2 <= OilResourcePercentage && OilResourcePercentage < .4)
            color = new Color32(78, 122, 36, 255);
        else if (.4 <= OilResourcePercentage && OilResourcePercentage < .6)
            color = new Color32(96, 186, 100, 255);
        else if (.6 <= OilResourcePercentage && OilResourcePercentage < .8)
            color = new Color32(96, 122, 100, 255);
        else
            color = new Color32(145, 187, 150, 255);

        if (drewOn == true)
            return (color + GetSelectedColor()) / 2;
        else
            return color;
    }

    public Color GetGoldColor() {
        Color color;
        

        if (GoldResorucePercentage < .2)
            color = Color.white;
        else if (.2 <= GoldResorucePercentage && GoldResorucePercentage < .4)
            color = new Color32(78, 122, 36, 255);
        else if (.4 <= GoldResorucePercentage && GoldResorucePercentage < .6)
            color = new Color32(96, 186, 100, 255);
        else if (.6 <= GoldResorucePercentage && GoldResorucePercentage < .8)
            color = new Color32(96, 122, 100, 255);
        else
            color = new Color32(145, 187, 150, 255);

        if (drewOn == true)
            return (color + GetSelectedColor()) / 2;
        else
            return color;
    }

    public Color GetWoodColor() {
        Color color;

        if (WoodResorucePercentage < .2)
            color =  Color.white;
        else if (.2 <= WoodResorucePercentage && WoodResorucePercentage < .36)
            color = new Color32(78, 122, 36, 255);
        else if (.36 <= WoodResorucePercentage && WoodResorucePercentage < .52)
            color = new Color32(96, 186, 100, 255);
        else if (.52 <= WoodResorucePercentage && WoodResorucePercentage < .68)
            color = new Color32(96, 122, 100, 255);
        else if (.68 <= WoodResorucePercentage && WoodResorucePercentage < .84)
            color = new Color32(96, 122, 100, 255);
        else
            color = new Color32(145, 187, 150, 255);

        if (drewOn == true)
            return (color + GetSelectedColor()) / 2;
        else
            return color;
    }

    public Color GetEthnicityColor() {
        Color color;

        if (EthinictyID.Equals("a"))
            color = new Color32(255, 218, 0, 255);
        else if (EthinictyID.Equals("b"))
            color = new Color32(255, 20, 142, 255);
        else if (EthinictyID.Equals("c"))
            color = new Color32(130, 0, 130, 255);
        else if (EthinictyID.Equals("d"))
            color = new Color32(255, 119, 165, 255);
        else if (EthinictyID.Equals("e"))
            color = new Color32(192, 9, 216, 255);
        else
            color = Color.white;

        if (drewOn == true)
            return (color + GetSelectedColor()) / 2;
        else
            return color;
    }

    public Color GetSelectedColor() {
        return Color.red;
    }
}
