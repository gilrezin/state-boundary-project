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

    public string EthinictyID = "";

    public bool IsSelected;
    public Pixel(double ElevationPercentage, double OilResourcePercentage, double GoldResourcePercentage, double WoodResourcePercentage, string EthnicityID, int[] Coordinate) {
        this.ElevationPercentage = ElevationPercentage;
        this.OilResourcePercentage = OilResourcePercentage;
        this.GoldResorucePercentage = GoldResourcePercentage;
        this.WoodResorucePercentage = WoodResourcePercentage;

        this.EthinictyID = EthnicityID;

        this.Coordinate = Coordinate;

    }

    public void ToggleSelect() {
        if (IsSelected)
            IsSelected = false;
        else
            IsSelected = true;
    }

    public Color GetLandColor() {

        if (IsSelected == true)
            return GetSelectedColor(); //If pixel is selected give red
        
        if (ElevationPercentage < .2)
            return new Color32(116, 183, 49, 255);
        else if (.2 <= ElevationPercentage && ElevationPercentage < .4)
            return new Color32(78, 122, 36, 255);
        else if (.4 <= ElevationPercentage && ElevationPercentage < .6)
            return new Color32(96, 186, 100, 255);
        else if (.6 <= ElevationPercentage && ElevationPercentage < .8)
            return new Color32(96, 122, 100, 255);
        else
            return new Color32(145, 187, 150, 255);
    }

    public Color32 GetOilColor() {
        if (IsSelected == true)
            return GetSelectedColor();//If pixel is selected give red

        if (OilResourcePercentage < .2)
            return Color.white;
        else if (.2 <= OilResourcePercentage && OilResourcePercentage < .4)
            return new Color32(78, 122, 36, 255);
        else if (.4 <= OilResourcePercentage && OilResourcePercentage < .6)
            return new Color32(96, 186, 100, 255);
        else if (.6 <= OilResourcePercentage && OilResourcePercentage < .8)
            return new Color32(96, 122, 100, 255);
        else
            return new Color32(145, 187, 150, 255);
    }

    public Color32 GetGoldColor() {
        if (IsSelected == true)
            return GetSelectedColor();//If pixel is selected give red

        if (GoldResorucePercentage < .2)
            return Color.white;
        else if (.2 <= GoldResorucePercentage && GoldResorucePercentage < .4)
            return new Color32(78, 122, 36, 255);
        else if (.4 <= GoldResorucePercentage && GoldResorucePercentage < .6)
            return new Color32(96, 186, 100, 255);
        else if (.6 <= GoldResorucePercentage && GoldResorucePercentage < .8)
            return new Color32(96, 122, 100, 255);
        else
            return new Color32(145, 187, 150, 255);
    }

    public Color32 GetWoodColor() {
        if (IsSelected == true)
            return GetSelectedColor();//If pixel is selected give red

        if (WoodResorucePercentage < .2)
            return Color.white;
        else if (.2 <= WoodResorucePercentage && WoodResorucePercentage < .36)
            return new Color32(78, 122, 36, 255);
        else if (.36 <= WoodResorucePercentage && WoodResorucePercentage < .52)
            return new Color32(96, 186, 100, 255);
        else if (.52 <= WoodResorucePercentage && WoodResorucePercentage < .68)
            return new Color32(96, 122, 100, 255);
        else if (.68 <= WoodResorucePercentage && WoodResorucePercentage < .84)
            return new Color32(96, 122, 100, 255);
        else
            return new Color32(145, 187, 150, 255);
    }

    public Color32 GetEthnicityColor() {
        if (IsSelected == true)
            return GetSelectedColor();//If pixel is selected give red

        if (EthinictyID.Equals("a"))
            return new Color32(255, 218, 0, 255);
        else if (EthinictyID.Equals("b"))
            return new Color32(255, 20, 142, 255);
        else if (EthinictyID.Equals("c"))
            return new Color32(130, 0, 130, 255);
        else if (EthinictyID.Equals("d"))
            return new Color32(255, 119, 165, 255);
        else if (EthinictyID.Equals("e"))
            return new Color32(192, 9, 216, 255);
        else
            return Color.white;
    }

    public Color32 GetSelectedColor() {
        return Color.red;
    }
}
