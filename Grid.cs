
using System;
using System.Collections.Generic;

namespace AStarVisualizer
{
    public class Grid
    {
        public int Width { get; }
        public int Height { get; }
        public Node[,] Nodes { get; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            Nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Nodes[x, y] = new Node(x, y);
                }
            }
        }

        public void GenerateRandomWalls(float density = 0.25f, Node? start = null, Node? target = null)
        {
            Random rand = new Random();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if ((start != null && x == start.X && y == start.Y) ||
                        (target != null && x == target.X && y == target.Y))
                    {
                        continue;
                    }

                    Nodes[x, y].IsWall = rand.NextDouble() < density;
                }
            }
        }

        public List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int checkX = node.X + dx[i];
                int checkY = node.Y + dy[i];

                if (checkX >= 0 && checkX < Width && checkY >= 0 && checkY < Height)
                {
                    neighbors.Add(Nodes[checkX, checkY]);
                }
            }

            return neighbors;
        }
    }
}
