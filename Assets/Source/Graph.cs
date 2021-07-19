using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
using Priority_Queue;


public class Graph : IEnumerable<Node>
{
    private List<Node> nodes;

    public Graph()
    {
        nodes = new List<Node>();
    }

    public void AddNode(Node node)
    {
        nodes.Add(node);

        foreach (var other in nodes)
        {
            if (other == node)
                continue;

            var hit = Physics2D.Linecast(node.Position, other.Position, LayerMask.GetMask("Walls"));
            if (hit.collider == null)
            {
                float distance = Vector2.Distance(node.Position, other.Position);

                node.Edges.Add(new Edge(other, distance, true));
                other.Edges.Add(new Edge(node, distance, false));
            }
        }
    }

    public void RemoveNode(Node node)
    {
        nodes.Remove(node);

        foreach (var edge in node.Edges)
        {
            for (int i = 0; i < edge.Node.Edges.Count; i++)
            {
                if (edge.Node.Edges[i].Node == node)
                {
                    edge.Node.Edges.RemoveAt(i);
                    break;
                }
            }
        }
        node.Edges.Clear();
    }

    public IList<Node> FindPath(Node start, Node end)
    {
        SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, float> cost = new Dictionary<Node, float>();

        frontier.Enqueue(start, 0);
        cameFrom.Add(start, null);
        cost.Add(start, 0);

        while (frontier.Any())
        {
            Node current = frontier.Dequeue();

            if (current == end)
                break;

            foreach (var edge in current.Edges)
            {
                float newCost = cost[current] + edge.Value;
                if (!cost.ContainsKey(edge.Node) || newCost < cost[edge.Node])
                {
                    frontier.Enqueue(edge.Node, newCost);
                    if (cost.ContainsKey(edge.Node))
                    {
                        cost[edge.Node] = newCost;
                        cameFrom[edge.Node] = current;
                    }
                    else
                    {
                        cost.Add(edge.Node, newCost);
                        cameFrom.Add(edge.Node, current);
                    }
                }
            }
        }

        List<Node> path = new List<Node>();
        Node currentNode = end;
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = cameFrom[currentNode];
        }
        path.Reverse();

        return path;
    }

    public IEnumerator<Node> GetEnumerator()
        => nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

}

public class Node
{
    public List<Edge> Edges { get; private set; }

    public Vector2 Position { get; private set; }

    public Node(Vector2 position)
    {
        Edges = new List<Edge>();
        Position = position;
    }
}

public class Edge
{
    public Node Node { get; private set; }

    public float Value { get; private set; }

    /// <summary>
    /// Determine if node should be rendered on screen.
    /// (Used for debuging purposes.)
    /// </summary>
    public bool Render { get; private set; }

    public Edge(Node node, float value, bool render)
    {
        Node = node;
        Value = value;
        Render = render;
    }
}
