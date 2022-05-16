using System;
using System.Collections.Generic;
using TestForHugProject;

public class App {
    public static Pixel[,]? World;

    public static Pixel[,] GenerateWorld(int xSize, int ySize, int MinimumNumberOfCirlces, int MaximumNumberOfCirlces) {


        int smoothSquareLength = new Random().Next(7, 52);

        int minimumCentreX = (int)Math.Round(xSize * -0.1);
        int maximumCentreX = (int)Math.Round(xSize * 1.1);

        int minimumCentreY = (int)Math.Round(ySize * -0.1);
        int maximumCentreY = (int)Math.Round(ySize * 1.1);

        int numberOfCenters = new Random().Next(MinimumNumberOfCirlces, MaximumNumberOfCirlces);


        int[,] centers = new int[numberOfCenters, 2];

        for (int i = 0; i < numberOfCenters; i++) {
            centers[i, 0] = randint(maximumCentreX);
            centers[i, 1] = randint(maximumCentreY);
        }



        double[,] data = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                List<Double> list = new List<Double>();
                for (int i = 0; i < (centers.Length) / 2; i++) {

                    list.Add(Math.Sqrt(Math.Pow(centers[i, 0] - 1 - x, 2) + Math.Pow(centers[i, 1] - 1 - y, 2)));
                }
                list.Sort();
                double weightedAverageDistanceFromCenter = list[0];
                data[x, y] = (randdouble(1) * (weightedAverageDistanceFromCenter / 2));
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

        Console.WriteLine("Number of centers: " + numberOfCenters);
        Console.WriteLine("Smooth Radius Check: " + smoothSquareLength);
        Console.WriteLine("Maximum Value: " + maxValue);
        Console.WriteLine("Minimum Value: " + minValue);

        Pixel[,] pixelArray = new Pixel[xSize, ySize];
        double[,] percentageData = new double[xSize, ySize];
        for (int x = 0; x < xSize; x++) {
            for (int y = 0; y < ySize; y++) {
                if (averagedData[x, y] >= average) {
                    percentageData[x, y] = (averagedData[x, y] - average) / (maxValue - average);

                }
                else
                    percentageData[x, y] = (averagedData[x, y] - average) / (minValue - average)*-1;

                pixelArray[x,y] = new Pixel(percentageData[x, y], 0, 0, 0, "", new int[] { x, y });
            }
        }

    


     
        return pixelArray;
    }

    public static double randdouble(double max) {
        Random random = new Random();
        return (max + .0000000001) * random.NextDouble();
    }
    public static int randint(int max) {
        Random random = new Random();
        return random.Next(max + 1);
    }

    static bool noDropRNG(double percentChance, int rolls) {
        for (int x = rolls; x != 0; x--) {
            Random random = new Random();

            double randomNum = random.NextDouble();// Get a Random  value from 0.00 - 1.00
            double adjustedPercentChance = percentChance;

            if (randomNum <= adjustedPercentChance / 100) {
                return true;
            }
        }
        return false;
    }
}
