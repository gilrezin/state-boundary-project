using UnityEngine;

public class Pixel {
    public double ElevationPercentage;

    public int[] Coordinate = new int[2];

    // Resources
    public double OilResourcePercentage;
    public double GoldResorucePercentage = 0;
    public double WoodResorucePercentage = 0;
    public bool drewOn = false;
    public bool border = false;

    public string EthinictyID = "";
    public Pixel(double ElevationPercentage, double OilResourcePercentage, double GoldResourcePercentage, double WoodResourcePercentage, string EthnicityID, int[] Coordinate) {
        this.ElevationPercentage = ElevationPercentage;
        this.OilResourcePercentage = OilResourcePercentage;
        this.GoldResorucePercentage = GoldResourcePercentage;
        this.WoodResorucePercentage = WoodResourcePercentage;

        this.EthinictyID = EthnicityID;

        this.Coordinate = Coordinate;

    }

    public bool HasGold() {
        return GoldResorucePercentage > .2;
    }

    public bool HasOil() {
        return OilResourcePercentage > .2;
    }

    public bool HasWood() {
        return WoodResorucePercentage > .2;
    }


    public Color GetLandColor() {
        Color color;

        if (ElevationPercentage < .2)
            color = new Color32(189, 207, 180, 255);
        else if (.2 <= ElevationPercentage && ElevationPercentage < .4)
            color = new Color32(156, 183, 143, 255);
        else if (.4 <= ElevationPercentage && ElevationPercentage < .6)
            color = new Color32(113, 149, 95, 255);
        else if (.6 <= ElevationPercentage && ElevationPercentage < .8)
            color = new Color32(85, 112, 72, 255);
        else
            color = new Color32(38, 50, 32, 255);

        if (drewOn)
            color = (color + GetSelectedColor()) / 2;
        if (border)
            color = (color + Color.black) / 2;
        return color;
    }

    public Color GetOilColor() {
        Color color;
        if (OilResourcePercentage < .2)
            color = Color.white;
        else
            color = new Color32(55, 0, 62, 255);

        if (drewOn)
            color = (color + GetSelectedColor()) / 2;
        if (border)
            color = (color + Color.black) / 2;
        return color;
    }

    public Color GetGoldColor() {
        Color color;


        if (GoldResorucePercentage < .2)
            color = Color.white;
        else
            color = new Color32(205, 183, 82, 255);

        if (drewOn)
            color = (color + GetSelectedColor()) / 2;
        if (border)
            color = (color + Color.black) / 2;
        return color;
    }

    public Color GetWoodColor() {
        Color color;

        if (WoodResorucePercentage < .2)
            color = Color.white;
        else if (.2 <= WoodResorucePercentage && WoodResorucePercentage < .36)
            color = new Color32(150, 175, 139, 255);
        else if (.36 <= WoodResorucePercentage && WoodResorucePercentage < .52)
            color = new Color32(117, 170, 90, 255);
        else if (.52 <= WoodResorucePercentage && WoodResorucePercentage < .68)
            color = new Color32(86, 170, 44, 255);
        else if (.68 <= WoodResorucePercentage && WoodResorucePercentage < .84)
            color = new Color32(58, 114, 29, 255);
        else
            color = new Color32(31, 61, 15, 255);

        if (drewOn)
            color = (color + GetSelectedColor()) / 2;
        if (border)
            color = (color + Color.black) / 2;
        return color;
    }

    public Color GetEthnicityColor() {
        Color color;

        if (EthinictyID.Equals("a"))
            color = new Color32(52, 52, 74, 255);
        else if (EthinictyID.Equals("b"))
            color = new Color32(128, 71, 95, 255);
        else if (EthinictyID.Equals("c"))
            color = new Color32(204, 90, 113, 255);
        else if (EthinictyID.Equals("d"))
            color = new Color32(200, 155, 123, 255);
        else if (EthinictyID.Equals("e"))
            color = new Color32(240, 247, 87, 255);
        else
            color = Color.white;

        if (drewOn)
            color = (color + GetSelectedColor()) / 2;
        if (border)
            color = (color + Color.black) / 2;
        return color;
    }

    public Color GetSelectedColor() {
        return Color.red;
    }
}
