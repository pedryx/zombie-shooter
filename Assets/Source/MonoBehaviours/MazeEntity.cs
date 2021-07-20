using System.Linq;

using UnityEngine;


/// <summary>
/// Represent a entity in maze.
/// </summary>
public class MazeEntity : MonoBehaviour
{
    /// <summary>
    /// Position after last current node change.
    /// </summary>
    private Vector2 lastPosition;

    public MazeGenerator Maze;

    /// <summary>
    /// Nearest node to entity.
    /// </summary>
    public Node CurrentNode { get; private set; }

    public void Init()
    {
        if (Maze == null)
            return;

        if (Maze.Graph.Any())
            CurrentNode = Maze.Graph.GetNearest(transform.position);
        Maze.OnMazeGenerated += Maze_OnMazeGenerated;
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, lastPosition) >= Maze.TileSize / 2)
        {
            CurrentNode = Maze.Graph.GetNearest(CurrentNode, transform.position);
            lastPosition = transform.position;
        }
    }

    private void Maze_OnMazeGenerated(object sender, System.EventArgs e)
    {
        CurrentNode = Maze.Graph.GetNearest(transform.position);
    }
}
