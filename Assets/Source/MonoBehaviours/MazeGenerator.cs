#define DEBUG_G

using System.Collections;
using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Represent a generator for a maze.
/// </summary>
public class MazeGenerator : MonoBehaviour
{
#if DEBUG_G
    /// <summary>
    /// Contains graphical represantion of nodes in graph.
    /// </summary>
    private List<GameObject> circles;
    /// <summary>
    /// Contains graphical represantion of edges in graph.
    /// </summary>
    private List<GameObject> lines;
    /// <summary>
    /// Determine if graph is visible.
    /// </summary>
    private bool showGraph = false;
    /// <summary>
    /// Last state of F1 key.
    /// </summary>
    private bool last = false;
#endif

    public Transform Player;
    public GameObject WallsPrefab;
    public GameObject DebugCirclePrefab;
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

    public Graph Graph { get; private set; }

    public MazeGenerator()
    {
        Graph = new Graph();
#if DEBUG_G
        circles = new List<GameObject>();
        lines = new List<GameObject>();
#endif
    }

    void Start()
    {
        totalWidth = Width * TileSize;
        totalHeight = Height * TileSize;

        CreateBorder();
        CreateMaze();
        StartCoroutine("CreateGraph");

        Player.position = new Vector3(-totalWidth / 2 + TileSize / 2, totalWidth / 2 - TileSize / 2);
    }

    private void Update()
    {
#if DEBUG_G
        bool current = Input.GetKey(KeyCode.F1);
        if (current && !last)
        {
            showGraph = !showGraph;
            foreach (var circle in circles)
                circle.SetActive(showGraph);
            foreach (var line in lines)
                line.SetActive(showGraph);
        }
        last = current;
#endif
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
        float posY = +totalHeight / 2 - y * TileSize;

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

    private IEnumerator CreateGraph()
    {
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2 center = new Vector2()
                {
                    x = -totalWidth / 2 + i * TileSize + TileSize / 2,
                    y = -totalHeight / 2 + j * TileSize + TileSize / 2,
                };
                CreateNode(center);
            }
        }
    }

    private void CreateNode(Vector2 pos)
    {
        var node = new Node(pos);
        Graph.AddNode(node);

#if DEBUG_G
        var circle = Instantiate(DebugCirclePrefab);
        circle.transform.position = pos;
        circle.SetActive(false);

        foreach (var edge in node.Edges)
        {
            if (edge.Render)
                CreateEdge(node.Position, edge.Node.Position);
        }

        circles.Add(circle);
#endif
    }

#if DEBUG_G
    private void CreateEdge(Vector2 start, Vector2 end)
    {
        var line = new GameObject();
        line.transform.position = start;
        line.AddComponent<LineRenderer>();

        var renderer = line.GetComponent<LineRenderer>();
        renderer.startWidth = .1f;
        renderer.endWidth = .1f;
        renderer.SetPositions(new Vector3[]{ start, end });
        renderer.useWorldSpace = true;
        renderer.material.color = Color.black;

        line.SetActive(false);
        lines.Add(line);
    }
#endif

}
