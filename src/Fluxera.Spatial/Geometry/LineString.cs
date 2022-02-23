// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="LineString" /> is an array of two or more positions.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.4
	/// </remarks>
	[PublicAPI]
	public struct LineString : IGeometry
	{
		public static readonly LineString Empty = new LineString();

		/// <summary>
		///     Creates a new instance of the <see cref="LineString" /> type.
		/// </summary>
		public LineString()
		{
			this.Coordinates = Array.Empty<Position>();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="LineString" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public LineString(IEnumerable<Position> coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Position>()).ToArray();

			if(this.Coordinates.Length < 2)
			{
				throw new ArgumentException(
					"A LineString must have at least two positions.",
					nameof(coordinates));
			}
		}

		/// <summary>
		///     Creates a new instance of the <see cref="LineString" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public LineString(params Position[] coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Position>()).ToArray();

			if(this.Coordinates.Length < 2)
			{
				throw new ArgumentException(
					"A LineString must have at least two positions.",
					nameof(coordinates));
			}
		}

		/// <summary>
		///     Creates a new instance of the <see cref="LineString" /> type.
		/// </summary>
		/// <param name="points">The points.</param>
		public LineString(IEnumerable<Point> points)
		{
			this.Coordinates = (points ?? Enumerable.Empty<Point>())
				.Select(x => (Position)x)
				.ToArray();

			if(this.Coordinates.Length < 2)
			{
				throw new ArgumentException(
					"A LineString must have at least two positions.",
					nameof(this.Coordinates));
			}
		}

		/// <summary>
		///     Creates a new instance of the <see cref="LineString" /> type.
		/// </summary>
		/// <param name="points">The points.</param>
		public LineString(params Point[] points)
		{
			this.Coordinates = (points ?? Enumerable.Empty<Point>())
				.Select(x => (Position)x)
				.ToArray();

			if(this.Coordinates.Length < 2)
			{
				throw new ArgumentException(
					"A LineString must have at least two positions.",
					nameof(this.Coordinates));
			}
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public Position[] Coordinates { get; }

		/// <summary>
		///     Checks if the line string is a linear ring.
		/// </summary>
		public bool IsLinearRing =>
			(this.Coordinates.Length >= 4) &&
			(this.Coordinates.First() == this.Coordinates.Last());

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not LineString lineString)
			{
				return false;
			}

			return this == lineString;
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
		///     Determines whether the specified line strings are considered equal.
		/// </summary>
		public static bool operator ==(LineString left, LineString right)
		{
			return left.Coordinates.SequenceEqual(right.Coordinates);
		}

		/// <summary>
		///     Determines whether the specified line strings are considered not equal.
		/// </summary>
		public static bool operator !=(LineString left, LineString right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public bool HasAltitude => this.Coordinates.Any(x => x.Altitude.HasValue);
	}
}
