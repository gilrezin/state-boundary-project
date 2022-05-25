using System;
using System.Collections.Generic;

public class WorldGenerator {
    public static Pixel[,] World;
    public static double[,] GeneratePercentageArray(int xSize, int ySize, int MinimumNumberOfCircles, int MaximumNumberOfCircles, int smoothSquareLength,  double AveragePercentageOffset) {

        int minimumCentreX = (int)Math.Round(xSize * -0.1);
        int maximumCentreX = (int)Math.Round(xSize * 1.1);

        int minimumCentreY = (int)Math.Round(ySize * -0.1);
        int maximumCentreY = (int)Math.Round(ySize * 1.1);

        int numberOfCenters = new Random().Next(MinimumNumberOfCircles, MaximumNumberOfCircles);


        int[,] centers = new int[numberOfCenters, 2];

        for (int i = 0; i < numberOfCenters; i++) {
            centers[i, 0] = new Random().Next(minimumCentreX, maximumCentreX);
            centers[i, 1] = new Random().Next(minimumCentreY,maximumCentreY);
        }



        double[,] data = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                List<Double> list = new();
                for (int i = 0; i < (centers.Length) / 2; i++) {

                    list.Add(Math.Sqrt(Math.Pow(centers[i, 0] - 1 - x, 2) + Math.Pow(centers[i, 1] - 1 - y, 2)));
                }
                list.Sort();
                double furthestDistance = list[0];
                data[x, y] = (new Random().NextDouble() * (furthestDistance / 2));
            }
        }



        double total = 0;
        int count = xSize * ySize;
        double maxValue = 0;
        double minValue = Double.MaxValue;


        double[,] averagedData = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                double a = 0;
                int c = 0;
                for (int xOffset = smoothSquareLength * -1; xOffset <= smoothSquareLength; xOffset++) {
                    for (int yOffset = smoothSquareLength * -1; yOffset <= smoothSquareLength; yOffset++) {
                        if (x + xOffset <= (xSize - 1) && y + yOffset <= (ySize - 1) && x + xOffset >= 0 && y + yOffset >= 0) {
                            a += data[x + xOffset, y + yOffset];
                            c++;
                        }
                    }
                }
                averagedData[x, y] = a / c;
                total += a / c;

                if (maxValue < a / c) {
                    maxValue = a / c;
                }
                if (minValue > a / c) {
                    minValue = a / c;
                }

            }
        }

        double average = total / count;


        double[,] percentageData = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                if (averagedData[x, y] <= average) {
                    percentageData[x, y] = (averagedData[x, y] - average * AveragePercentageOffset) / (maxValue - average * AveragePercentageOffset)* -1;

                }
                else
                    percentageData[x, y] = (averagedData[x, y] - average * AveragePercentageOffset) / (minValue - average * AveragePercentageOffset);
            }
        }

        return percentageData;
    }

    public static Pixel[,] GenerateWorld(int xSize, int ySize, int MinimumNumberOfCircles, int MaximumNumberOfCircles) {



        Pixel[,] pixelArray = new Pixel[xSize, ySize];
        double[,] landPercentageData = GeneratePercentageArray(xSize, ySize, MinimumNumberOfCircles, MaximumNumberOfCircles, new Random().Next(7, 52), 1.1d);
        double[,] oilPercentageData = GeneratePercentageArray(xSize, ySize, MinimumNumberOfCircles*40, MaximumNumberOfCircles * 40, new Random().Next(1, 10), .95d);
        double[,] goldPercentageData = GeneratePercentageArray(xSize, ySize, MinimumNumberOfCircles * 40, MaximumNumberOfCircles * 40, new Random().Next(1, 60), .95d);
        double[,] woodPercentageData = GeneratePercentageArray(xSize, ySize, MinimumNumberOfCircles, MaximumNumberOfCircles, new Random().Next(5, 40), 1.3d);
        double[,] ethnicityPercentageData = GeneratePercentageArray(xSize, ySize, MinimumNumberOfCircles, MaximumNumberOfCircles, new Random().Next(10, 30), 1d);
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                double pixelWoodCover;
                if (landPercentageData[x, y] < 0)
                    pixelWoodCover = -1;
                else
                    pixelWoodCover = woodPercentageData[x, y];
                string ethnityID;
                if (-1 <= ethnicityPercentageData[x, y] && ethnicityPercentageData[x, y] < -.6) 
                    ethnityID = "a";
                else if (-.6 <= ethnicityPercentageData[x, y] && ethnicityPercentageData[x, y] < -.2)
                    ethnityID = "b";
                else if (-.2 <= ethnicityPercentageData[x, y] && ethnicityPercentageData[x, y] < .2)
                    ethnityID = "c";
                else if (.2 <= ethnicityPercentageData[x, y] && ethnicityPercentageData[x, y] < .6)
                    ethnityID = "d";
                else
                    ethnityID = "e";
                if(landPercentageData[x, y] >= 0)
                pixelArray[x, y] = new Pixel(landPercentageData[x, y], oilPercentageData[x,y], goldPercentageData[x,y], pixelWoodCover, ethnityID, new int[] { x, y });

            }
        }

        return pixelArray;
    }

}
