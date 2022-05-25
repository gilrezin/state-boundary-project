
public class World
{
    public static Pixel[,] world;
    public static string currentView;
    public static Pixel[,] GetWorld() {
        return world;
    }

    public static void SetWorld(Pixel[,] SetWorld) {
        world = SetWorld;
    }
}
