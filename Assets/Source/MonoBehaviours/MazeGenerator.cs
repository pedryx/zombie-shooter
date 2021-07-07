using UnityEngine;


/// <summary>
/// Represent a generator for a maze.
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    public Transform Player;
    public GameObject FloorPrefab;
    public GameObject WallsPrefab;
    /// <summary>
    /// Maze width in tiles.
    /// </summary>
    public int Width;
    /// <summary>
    /// Maze height in tiles.
    /// </summary>
    public int Height;
    public float TileSize;
    public float WallWidth;

    private float totalWidth;
    private float totalHeight;

    void Start()
    {
        totalWidth = Width * TileSize;
        totalHeight = Height * TileSize;

        CreateFloor();
        CreateBorder();
        CreateMaze();

        Player.position = new Vector3(-totalWidth / 2 + TileSize / 2, totalWidth / 2 - TileSize / 2);
    }

    /// <summary>
    /// Create maze floor.
    /// </summary>
    private void CreateFloor()
    {
        var floor = Instantiate(FloorPrefab, transform);
        floor.transform.localScale = new Vector3(totalWidth, totalHeight, 1);
    }

    /// <summary>
    /// Create wall border around the maze.
    /// </summary>
    private void CreateBorder()
    {
        for (int i = 0; i < Width; i++)
        {
            CreateWall(i, 0, Direction.Right);
            CreateWall(i, Height, Direction.Right);
        }

        for (int i = 0; i < Height; i++)
        {
            CreateWall(0, i, Direction.Down);
            CreateWall(Width, i, Direction.Down);
        }
    }

    /// <summary>
    /// Generate maze.
    /// </summary>
    private void CreateMaze()
    {
        System.Random random = new System.Random();

        for (int i = 1; i < Width; i++)
        {
            for (int j = 1; j < Height; j++)
                CreateWall(i, j, (Direction)random.Next(4));
        }
    }

    private void CreateWall(int x, int y, Direction dir)
    {
        var wall = Instantiate(WallsPrefab, transform);
        float posX = -totalWidth / 2 + x * TileSize;
        float posY = +totalWidth / 2 - y * TileSize;

        switch (dir)
        {
            case Direction.Up:
                wall.transform.position = new Vector3(posX, posY + TileSize / 2);
                wall.transform.localScale = new Vector3(WallWidth, TileSize + WallWidth, 1);
                break;
            case Direction.Right:
                wall.transform.localPosition = new Vector3(posX + TileSize / 2, posY);
                wall.transform.localScale = new Vector3(TileSize + WallWidth, WallWidth, 1);
                break;
            case Direction.Down:
                wall.transform.position = new Vector3(posX, posY - TileSize / 2);
                wall.transform.localScale = new Vector3(WallWidth, TileSize + WallWidth, 1);
                break;
            case Direction.Left:
                wall.transform.localPosition = new Vector3(posX - TileSize / 2, posY);
                wall.transform.localScale = new Vector3(TileSize + WallWidth, WallWidth, 1);
                break;
        }

        
    }

}
