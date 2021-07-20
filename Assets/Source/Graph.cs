using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
using Priority_Queue;
using System;

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

            if (PhysicsHelper.CirclesInSight(node.Position, .5f, other.Position, .5f))
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

    public Node GetNearest(Vector2 position)
    {
        Node minNode = nodes[0];
        float minDistance = Vector2.Distance(position, minNode.Position);

        for (int i = 1; i < nodes.Count; i++)
        {
            float distance = Vector2.Distance(position, nodes[i].Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minNode = nodes[i];
            }
        }

        return minNode;
    }

    public Node GetNearest(Node node, Vector2 position)
    {
        Node minNode = node;
        float minDistance = Vector2.Distance(position, node.Position);

        foreach (var edge in node.Edges)
        {
            float distance = Vector2.Distance(position, edge.Node.Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minNode = edge.Node;
            }
        }

        return minNode;
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

    private Color color;

    public Color Color
    {
        get => color;
        set 
        {
            color = value;
            OnColorChange?.Invoke(this, new EventArgs());
        }
    }

    public event EventHandler OnColorChange;

    public Edge(Node node, float value, bool render)
    {
        Node = node;
        Value = value;
        Render = render;
    }

}
