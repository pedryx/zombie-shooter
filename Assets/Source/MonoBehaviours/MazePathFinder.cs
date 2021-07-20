using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Represent a path finder in for MazeGenerator component.
/// </summary>
public class MazePathFinder : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
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
    /// <summary>
    /// Position toward this entity is currently going.
    /// </summary>
    private Vector2 nextPosition;
    /// <summary>
    /// The node where was target on last path-finding.
    /// </summary>
    private Node lastTargetNode;

    /// <summary>
    /// Maze for path-finding.
    /// </summary>
    public MazeGenerator Maze;
    /// <summary>
    /// Target entity to path-find.
    /// </summary>
    public MazeEntity Target;
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Determine if entity is able to see the target.
    /// </summary>
    public bool TargetInSight { get; private set; }

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PhysicsHelper.CirclesInSight(transform.position, .5f,
            Target.transform.position, .5f))
        {
            nextPosition = Target.transform.position;
            TargetInSight = true;
        }
        else
        {
            if (lastTargetNode != Target.CurrentNode || TargetInSight)
            {
                path = Maze.Graph.FindPath(gameObject.GetComponent<MazeEntity>().CurrentNode,
                    Target.CurrentNode);
                lastTargetPos = Target.transform.position;
                lastTargetNode = Target.CurrentNode;
                pathPosition = 1;
                TargetInSight = false;
            }

            nextPosition = path[pathPosition].Position;
        }
    }

    private void FixedUpdate()
    {
        if (path == null || pathPosition >= path.Count)
            return;

        Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
        Vector2 move = direction * Speed * Time.deltaTime;

        Vector2 newPosition;
        if (Vector2.Distance(transform.position, nextPosition) <= move.magnitude || move.magnitude == 0)
        {
            newPosition = nextPosition;
            if (!TargetInSight)
            {
                pathPosition++;
                nextPosition = path[pathPosition].Position;
            }
        }
        else
            newPosition = (Vector2)transform.position + move;

        rigidbody.MovePosition(newPosition);
    }

}
