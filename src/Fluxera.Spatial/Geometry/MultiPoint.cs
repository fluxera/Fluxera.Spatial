// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="MultiPoint" /> is an array of positions.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.3
	/// </remarks>
	[PublicAPI]
	public struct MultiPoint : IGeometry
	{
		public static readonly MultiPoint Empty = new MultiPoint();

		/// <summary>
		///     Creates a new instance of the <see cref="MultiPoint" /> type.
		/// </summary>
		public MultiPoint()
		{
			this.Coordinates = Array.Empty<Position>();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiPoint" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public MultiPoint(IEnumerable<Position> coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Position>()).ToArray();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiPoint" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public MultiPoint(params Position[] coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Position>()).ToArray();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiPoint" /> type.
		/// </summary>
		/// <param name="points">The points.</param>
		public MultiPoint(IEnumerable<Point> points)
		{
			this.Coordinates = (points ?? Enumerable.Empty<Point>())
				.Select(x => (Position)x)
				.ToArray();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiPoint" /> type.
		/// </summary>
		/// <param name="points">The points.</param>
		public MultiPoint(params Point[] points)
		{
			this.Coordinates = (points ?? Enumerable.Empty<Point>())
				.Select(x => (Position)x)
				.ToArray();
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public Position[] Coordinates { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not MultiPoint multiPoint)
			{
				return false;
			}

			return this == multiPoint;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			foreach(Position position in this.Coordinates)
			{
				hashCode = (hashCode * 37) ^ position.GetHashCode();
			}

			return hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Coordinates.Any()
				? this.Coordinates.Select(x => x.ToString()).Aggregate((s1, s2) => $"{s1}{Environment.NewLine}{s2}")
				: string.Empty;
		}

		/// <summary>
		///     Determines whether the specified multi points are considered equal.
		/// </summary>
		public static bool operator ==(MultiPoint left, MultiPoint right)
		{
			return left.Coordinates.SequenceEqual(right.Coordinates);
		}

		/// <summary>
		///     Determines whether the specified multi points are considered not equal.
		/// </summary>
		public static bool operator !=(MultiPoint left, MultiPoint right)
		{
			return !(left == right);
		}
	}
}
