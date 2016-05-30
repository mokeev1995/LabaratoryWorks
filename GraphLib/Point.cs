using System;

namespace GraphLib
{
	public class Point : IEquatable<Point>
	{
		public Point(int number)
		{
			Number = number;
		}

		public int Number { get; }
		
		public bool Equals(Point other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Number == other.Number;
		}

		public static bool operator ==(Point first, Point second)
		{
			if (ReferenceEquals(null, first) && ReferenceEquals(null, second)) return true;
			if (ReferenceEquals(null, first) || ReferenceEquals(null, second)) return false;
			return ReferenceEquals(first, second) || first.Equals(second);
		}

		public static bool operator !=(Point first, Point second)
		{
			return !(first == second);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Point) obj);
		}

		public override int GetHashCode()
		{
			return Number;
		}

		public override string ToString()
		{
			return $"Point: {Number}";
		}
	}
}