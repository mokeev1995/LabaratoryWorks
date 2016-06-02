using System;
using System.Linq;

namespace GraphLib
{
	public class Edge : IEquatable<Edge>, IComparable<Edge>, IComparable
	{
		public Edge(Point from, Point to, double weight)
		{
			From = from;
			To = to;
			Weight = weight;
		}

		public Point From { get; }
		public Point To { get; }
		public double Weight { get; }

		public int State
		{
			get
			{
				if (From.AddedMark == To.AddedMark)
					return From.AddedMark;

				return -2;
			}
			set
			{
				From.AddedMark = value;
				To.AddedMark = value;
			}
		}

		public bool Equals(Edge other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(From, other.From) && Equals(To, other.To) && Weight.Equals(other.Weight);
		}

		public bool ContainsPoints(params Point[] points)
		{
			return points.All(point => point == From || point == To);
		}

		public static bool operator ==(Edge first, Edge second)
		{
			if (ReferenceEquals(null, first) && ReferenceEquals(null, second)) return true;
			if (ReferenceEquals(null, first) || ReferenceEquals(null, second)) return false;
			return ReferenceEquals(first, second) || first.Equals(second);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Edge) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = From?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (To?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Weight.GetHashCode();
				return hashCode;
			}
		}

		public int CompareTo(object obj)
		{
			return CompareTo((Edge) obj);
		}

		public int CompareTo(Edge otherEdge)
		{
			return Weight.CompareTo(otherEdge.Weight);
		}

		public static bool operator !=(Edge first, Edge second)
		{
			return !(first == second);
		}

		public override string ToString()
		{
			return $"From: {From}, To: {To}, Weight: {Weight}";
		}

		public bool ContainsPoint(params Point[] points)
		{
			return points.Any(point => point == From || point == To);
		}
	}
}