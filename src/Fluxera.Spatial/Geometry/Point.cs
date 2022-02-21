// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="Point" /> is a single position.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.2
	/// </remarks>
	[PublicAPI]
	public struct Point : IGeometry
	{
		public static readonly Point Empty = new Point();

		/// <summary>
		///     Creates a new instance of the <see cref="Point" /> type.
		/// </summary>
		public Point()
		{
			this.Coordinates = Position.Empty;
		}

		/// <summary>
		///     Creates a new instance of the <see cref="Point" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public Point(Position coordinates)
		{
			this.Coordinates = coordinates;
		}

		/// <summary>
		///     Creates a new instance of the <see cref="Point" /> type.
		/// </summary>
		/// <param name="point">The point to copy.</param>
		public Point(Point point)
		{
			this.Coordinates = new Position(point.Longitude, point.Latitude, point.Altitude);
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public Position Coordinates { get; }

		/// <summary>
		///     Gets the longitude (x coordinate).
		/// </summary>
		public double Longitude => this.Coordinates.Longitude;

		/// <summary>
		///     Gets the latitude (y coordinate).
		/// </summary>
		public double Latitude => this.Coordinates.Latitude;

		/// <summary>
		///     Gets the altitude (z coordinate) in meters.
		/// </summary>
		public double? Altitude => this.Coordinates.Altitude;

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return this == (Point)obj;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return this.Coordinates.GetHashCode();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Coordinates.ToString();
		}

		/// <summary>
		///     Determines whether the specified points are considered equal.
		/// </summary>
		public static bool operator ==(Point left, Point right)
		{
			return left.Coordinates == right.Coordinates;
		}

		/// <summary>
		///     Determines whether the specified points are considered not equal.
		/// </summary>
		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}

		/// <summary>
		///     Converts the given <see cref="Position" /> to a <see cref="Point" />.
		/// </summary>
		/// <param name="position">The position.</param>
		public static explicit operator Point(Position position)
		{
			return new Point(position);
		}

		/// <summary>
		///     Converts the given <see cref="Position" /> to a <see cref="Point" />.
		/// </summary>
		/// <param name="point">The point.</param>
		public static explicit operator Position(Point point)
		{
			return new Position(point.Longitude, point.Latitude, point.Altitude);
		}
	}
}
