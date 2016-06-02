using System;
using System.Collections.Generic;
using System.Linq;
using BinaryHeapLib;

namespace GraphLib
{
	public class Graph
	{
		private readonly Point[] _poits;
		private readonly BinaryHeap<Edge> _edges;
		private int PointsCount { get; }

		private Graph(BinaryHeap<Edge> edges, int pointsCount, Point[] poits)
		{
			_edges = edges;
			PointsCount = pointsCount;
			_poits = poits;
		}

		public static Graph Build(string[] file)
		{
			if (file.Length < 1)
				throw new ArgumentException("No info in file!");

			var count = int.Parse(file[0]);

			if (file.Length < count + 1)
				throw new ArgumentException("Wrong lines count!");

			var points = Enumerable.Range(0, count).Select(i => new Point(i)).ToArray();

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

					var from = points[i - 1];
					var to = points[position];
					if (!edges.Any(edge => edge.ContainsPoints(from, to)))
					{
						edges.Add(new Edge(from, to, weight));
					}
				}
			}

			return new Graph(edges, count, points);
		}

		public MinimumSpanningTree GetMst()
		{
			var selectedItems = new List<Edge>();

			var source = new MinBinaryHeap<Edge>(_edges);

			var mark = 1;

			while (source.Count > 0)
			{
				var element = source.GetTopElement();
				source.DeleteTopElement();

				if (!CycleFound(selectedItems, element))
				{
					selectedItems.Add(element);
					var localMark = mark;
					var foundEdge = selectedItems.Where(
						edge =>
							edge.From.Number == element.From.Number || edge.To.Number == element.From.Number ||
							edge.From.Number == element.To.Number || edge.To.Number == element.To.Number)
						.Where(edge => edge.From != element.From && edge.To != element.To)
						.ToArray();

					var mrkItems = foundEdge.Where(edge => edge.From.AddedMark > 0).ToArray();
					if (mrkItems.Any())
					{
						localMark = mrkItems.Min(edge => edge.From.AddedMark);
					}
					var edges = selectedItems.Where(edge => edge.ContainsPoint(element.From, element.To));
					foreach (var edge in edges)
					{
						edge.State = localMark;
					}
					element.State = localMark;
					mark++;
				}
			}

			return new MinimumSpanningTree(selectedItems.SelectMany(edge => new[] {edge.From, edge.To}).Distinct().ToArray(), selectedItems.Sum(edge => edge.Weight));
		}

		private static bool CycleFound(IEnumerable<Edge> edgesAdded, Edge toAdd)
		{
			foreach (var edge in edgesAdded)
			{
				if (IsSameConnectedComponent(edge, toAdd))
					return true;
			}
			return false;
		}

		private static bool IsSameConnectedComponent(Edge edge, Edge toAdd)
		{
			var firstCase = toAdd.State == edge.State && toAdd.State > 0;
			return firstCase;
		}
	}

	public class MinimumSpanningTree
	{
		public IEnumerable<Point> PointsRoute { get; }
		public double Weight { get; }

		public MinimumSpanningTree(IEnumerable<Point> pointsRoute, double weight)
		{
			PointsRoute = pointsRoute;
			Weight = weight;
		}
	}
}