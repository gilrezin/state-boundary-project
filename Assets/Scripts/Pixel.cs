using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForHugProject {


    public class Pixel {
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

        public void toggleSelect() {
            if (IsSelected)
                IsSelected = false;
            else
                IsSelected = true;
        }
    }
}
