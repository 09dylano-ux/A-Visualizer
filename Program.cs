
using System;
using System.Collections.Generic;
using System.Threading;

namespace AStarVisualizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            int width = 40;
            int height = 15;

            Grid grid = new Grid(width, height);
            Node startNode = grid.Nodes[2, 7];
            Node targetNode = grid.Nodes[37, 7];

            grid.GenerateRandomWalls(0.22f, startNode, targetNode);

            HashSet<Node> openSetLookup = new HashSet<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            MinHeap openSet = new MinHeap();

            startNode.GCost = 0;
            startNode.HCost = GetManhattanDistance(startNode, targetNode);
            openSet.Add(startNode);
            openSetLookup.Add(startNode);

            bool pathFound = false;

            Console.Clear();
            Console.WriteLine("=== A* Pathfinding Visualizer (C#) ===");
            Console.WriteLine("S: Start | T: Target | #: Wall | *: Open | x: Closed | O: Path\n");

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                openSetLookup.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathFound = true;
                    break;
                }

                foreach (Node neighbor in grid.GetNeighbors(currentNode))
                {
                    if (neighbor.IsWall || closedSet.Contains(neighbor))
                        continue;

                    float newMovementCost = currentNode.GCost + 1;
                    if (newMovementCost < neighbor.GCost)
                    {
                        neighbor.GCost = newMovementCost;
                        neighbor.HCost = GetManhattanDistance(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openSetLookup.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                            openSetLookup.Add(neighbor);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbor);
                        }
                    }
                }

                RenderGrid(grid, startNode, targetNode, openSetLookup, closedSet, null);
                Thread.Sleep(30);
            }

            List<Node>? finalPath = null;
            if (pathFound)
            {
                finalPath = RetracePath(startNode, targetNode);
            }

            RenderGrid(grid, startNode, targetNode, openSetLookup, closedSet, finalPath);

            Console.SetCursorPosition(0, height + 4);
            if (pathFound)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nPath found successfully! Length: {finalPath!.Count} steps.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo path possible due to obstacle blockage.");
            }

            Console.ResetColor();
            Console.CursorVisible = true;
        }

        private static float GetManhattanDistance(Node nodeA, Node nodeB)
        {
            return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
        }

        private static List<Node> RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node? currentNode = targetNode;

            while (currentNode != null && currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        private static void RenderGrid(Grid grid, Node start, Node target, HashSet<Node> openSet, HashSet<Node> closedSet, List<Node>? path)
        {
            HashSet<Node> pathLookup = path != null ? new HashSet<Node>(path) : new HashSet<Node>();

            for (int y = 0; y < grid.Height; y++)
            {
                Console.SetCursorPosition(0, y + 3);
                for (int x = 0; x < grid.Width; x++)
                {
                    Node node = grid.Nodes[x, y];

                    if (node == start)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("S ");
                    }
                    else if (node == target)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("T ");
                    }
                    else if (pathLookup.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("O ");
                    }
                    else if (node.IsWall)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("# ");
                    }
                    else if (openSet.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("* ");
                    }
                    else if (closedSet.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("x ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(". ");
                    }
                }
            }
            Console.ResetColor();
        }
    }
}
