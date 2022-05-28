// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="GeometryCollection" /> is an array of <see cref="IGeometry" /> objects.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.8
	/// </remarks>
	[PublicAPI]
	public struct GeometryCollection : IGeometry
	{
		/// <summary>
		///     Represents an empty <see cref="GeometryCollection" />.
		/// </summary>
		public static readonly GeometryCollection Empty = new GeometryCollection();

		/// <summary>
		///     Creates a new instance of the <see cref="GeometryCollection" /> type.
		/// </summary>
		public GeometryCollection()
		{
			this.Geometries = Array.Empty<IGeometry>();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="GeometryCollection" /> type.
		/// </summary>
		/// <param name="geometries">The geometries.</param>
		public GeometryCollection(IEnumerable<IGeometry> geometries)
		{
			this.Geometries = (geometries ?? Enumerable.Empty<IGeometry>()).ToArray();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="GeometryCollection" /> type.
		/// </summary>
		/// <param name="geometries">The geometries.</param>
		public GeometryCollection(params IGeometry[] geometries)
		{
			this.Geometries = (geometries ?? Enumerable.Empty<IGeometry>()).ToArray();
		}

		/// <summary>
		///     Gets the geometries.
		/// </summary>
		public IGeometry[] Geometries { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not GeometryCollection geometryCollection)
			{
				return false;
			}

			return this == geometryCollection;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			foreach(IGeometry geometryObject in this.Geometries)
			{
				hashCode = hashCode * 37 ^ geometryObject.GetHashCode();
			}

			return hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Geometries.Any()
				? this.Geometries.Select(x => x.ToString()).Aggregate((s1, s2) => $"{s1}{Environment.NewLine}{s2}")
				: string.Empty;
		}

		/// <summary>
		///     Determines whether the specified line strings are considered equal.
		/// </summary>
		public static bool operator ==(GeometryCollection left, GeometryCollection right)
		{
			return left.Geometries.SequenceEqual(right.Geometries);
		}

		/// <summary>
		///     Determines whether the specified line strings are considered not equal.
		/// </summary>
		public static bool operator !=(GeometryCollection left, GeometryCollection right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public bool HasAltitude => this.Geometries.Any(x => x.HasAltitude);
	}
}
