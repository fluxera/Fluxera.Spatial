// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="Polygon" /> is an array of linear rings. A linear
	///     ring is a closed line ring with four or more positions, where
	///     the first and the last positions are identical.
	/// </summary>
	[PublicAPI]
	public struct Polygon : IGeometry
	{
		/// <summary>
		///     Represents an empty <see cref="Polygon" />.
		/// </summary>
		public static readonly Polygon Empty = new Polygon();

		/// <summary>
		///     Creates a new instance of the <see cref="Polygon" /> type.
		/// </summary>
		public Polygon()
		{
			this.Coordinates = Array.Empty<LineString>();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="Polygon" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public Polygon(IEnumerable<LineString> coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<LineString>()).ToArray();

			if(this.Coordinates.Any(x => !x.IsLinearRing))
			{
				throw new ArgumentException(
					"A Polygon must be of LineStrings that are linear rings.",
					nameof(coordinates));
			}
		}

		/// <summary>
		///     Creates a new instance of the <see cref="Polygon" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public Polygon(params LineString[] coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<LineString>()).ToArray();

			if(this.Coordinates.Any(x => !x.IsLinearRing))
			{
				throw new ArgumentException(
					"A Polygon must be of LineStrings that are linear rings.",
					nameof(coordinates));
			}
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public LineString[] Coordinates { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not Polygon polygon)
			{
				return false;
			}

			return this == polygon;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = 17;

			foreach(LineString lineString in this.Coordinates)
			{
				hashCode = hashCode * 37 ^ lineString.GetHashCode();
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
		///     Determines whether the specified polygons are considered equal.
		/// </summary>
		public static bool operator ==(Polygon left, Polygon right)
		{
			return left.Coordinates.SequenceEqual(right.Coordinates);
		}

		/// <summary>
		///     Determines whether the specified polygons are considered not equal.
		/// </summary>
		public static bool operator !=(Polygon left, Polygon right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public bool HasAltitude => this.Coordinates.Any(x => x.HasAltitude);
	}
}
