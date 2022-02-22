// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="MultiPolygon" /> is an array of polygons.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.7
	/// </remarks>
	[PublicAPI]
	public struct MultiPolygon : IGeometry
	{
		public static readonly MultiPolygon Empty = new MultiPolygon();

		/// <summary>
		///     Create a new instance of the <see cref="MultiPolygon" /> type.
		/// </summary>
		public MultiPolygon()
		{
			this.Coordinates = Array.Empty<Polygon>();
		}

		/// <summary>
		///     Create a new instance of the <see cref="MultiPolygon" /> type.
		/// </summary>
		/// <param name="coordinates"></param>
		public MultiPolygon(IEnumerable<Polygon> coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Polygon>()).ToArray();
		}

		/// <summary>
		///     Create a new instance of the <see cref="MultiPolygon" /> type.
		/// </summary>
		/// <param name="coordinates"></param>
		public MultiPolygon(params Polygon[] coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<Polygon>()).ToArray();
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public Polygon[] Coordinates { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not MultiPolygon multiPolygon)
			{
				return false;
			}

			return this == multiPolygon;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			foreach(Polygon polygon in this.Coordinates)
			{
				hashCode = (hashCode * 37) ^ polygon.GetHashCode();
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
		///     Determines whether the specified multi polygons are considered equal.
		/// </summary>
		public static bool operator ==(MultiPolygon left, MultiPolygon right)
		{
			return left.Coordinates.SequenceEqual(right.Coordinates);
		}

		/// <summary>
		///     Determines whether the specified multi polygons are considered not equal.
		/// </summary>
		public static bool operator !=(MultiPolygon left, MultiPolygon right)
		{
			return !(left == right);
		}
	}
}
