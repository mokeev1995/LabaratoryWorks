using System;
using System.Collections.Generic;
using System.Linq;
using BinaryHeapLib;

namespace GraphLib
{
	public class Graph
	{
		private ICollection<Edge> _edges;
		private int PointsCount { get; set; }

		private Graph(ICollection<Edge> edges, int pointsCount)
		{
			_edges = edges;
			PointsCount = pointsCount;
		}

		public static Graph Build(string[] file)
		{
			if (file.Length < 1)
				throw new ArgumentException("No info in file!");

			var count = int.Parse(file[0]);

			if (file.Length < count + 1)
				throw new ArgumentException("Wrong lines count!");

			var points = new Point[count];

			for (var i = 0; i < count; i++)
			{
				points[i] = new Point(i);
			}

			var edges = new MinBinaryHeap<Edge>();

			for (var i = 1; i < count + 1; i++)
			{
				var positions =
					file[i].Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
						.Select(s => s.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
						.ToArray();

				foreach (var positionInString in positions)
				{
					var position = int.Parse(positionInString[0]);
					var weight = int.Parse(positionInString[1]);

					if (!edges.Any(edge => edge.ContainsPoints(points[i - 1], points[position])))
					{
						edges.Add(new Edge(points[i - 1], points[position], weight));
					}
				}
			}

			return new Graph(edges, count);
		}
	}
}