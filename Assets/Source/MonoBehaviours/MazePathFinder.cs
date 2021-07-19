using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Represent a path finder in for MazeGenerator component.
/// </summary>
public class MazePathFinder : MonoBehaviour
{
    /// <summary>
    /// Position of target after last path-finding.
    /// </summary>
    private Vector2 lastTargetPos;
    /// <summary>
    /// Current path.
    /// </summary>
    private IList<Node> path;
    /// <summary>
    /// Index of last visited node in current path.
    /// </summary>
    private int pathPosition;
    private new Rigidbody2D rigidbody;

    public Transform Target;
    public MazeGenerator Maze;
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float Speed;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 currentTargetPos = Target.position;

        if (Vector2.Distance(currentTargetPos, lastTargetPos) > 1)
        {
            CreatePath(Maze.Graph, currentTargetPos, transform.position);
            lastTargetPos = currentTargetPos;
            pathPosition = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null || pathPosition + 1 >= path.Count)
            return;

        Vector2 newPos;
        Vector2 direction = (path[pathPosition + 1].Position - (Vector2)transform.position).normalized;
        Vector2 move = direction * Speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, path[pathPosition + 1].Position) <= move.magnitude + .1f)
        {
            newPos = path[pathPosition + 1].Position;
            pathPosition++;
        }
        else
        {
            newPos = (Vector2)transform.position + move;
        }

        rigidbody.MovePosition(newPos);
    }

    /// <summary>
    /// Create new path from pos to target on graph.
    /// </summary>
    private void CreatePath(Graph graph, Vector2 target, Vector2 pos)
    {
        Node targetNode = new Node(target);
        Node posNode = new Node(pos);

        graph.AddNode(targetNode);
        graph.AddNode(posNode);

        path = graph.FindPath(posNode, targetNode);

        graph.RemoveNode(targetNode);
        graph.RemoveNode(posNode);
    }

}
