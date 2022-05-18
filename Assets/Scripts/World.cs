using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public static Pixel[,] world;
    public static Pixel[,] GetWorld() {
        return world;
    }

    public static void SetWorld(Pixel[,] SetWorld) {
        world = SetWorld;
    }
}
