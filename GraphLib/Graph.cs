using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib
{
	public class Graph
	{
		private IEnumerable<Edge> _edges;

		private Graph(IEnumerable<Edge> edges)
		{
			_edges = edges;
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

			var edges = new List<Edge>(count*(count - 1)/2);

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

			return new Graph(edges);
		}
	}
}