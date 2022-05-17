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

    public Color32 GetLandColor() {
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
}
