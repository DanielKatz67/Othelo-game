namespace Ex02_Othelo;

public static class Constants
{
    public static int[,] directions = new int[,]
    {
        {-1,  0}, // Left
        { 1,  0}, // Right
        { 0, -1}, // Up
        { 0,  1}, // Down
        {-1, -1}, // Top-left diagonal
        { 1,  1}, // Bottom-right diagonal
        {-1,  1}, // Bottom-left diagonal
        { 1, -1}  // Top-right diagonal
    };
}