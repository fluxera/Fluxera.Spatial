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
		private readonly IGeometry[] geometries = Array.Empty<IGeometry>();

		/// <summary>
		///     Creates a new instance of the <see cref="GeometryCollection" /> type.
		/// </summary>
		public GeometryCollection(IEnumerable<IGeometry> geometries)
		{
			this.geometries = (geometries ?? Enumerable.Empty<IGeometry>()).ToArray();
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return this == (GeometryCollection)obj;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

			foreach(IGeometry geometryObject in this.geometries)
			{
				hashCode = (hashCode * 37) ^ geometryObject.GetHashCode();
			}

			return hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.geometries.Any()
				? this.geometries.Select(x => x.ToString()).Aggregate((s1, s2) => $"{s1}{Environment.NewLine}{s2}")
				: string.Empty;
		}

		/// <summary>
		///     Determines whether the specified line strings are considered equal.
		/// </summary>
		public static bool operator ==(GeometryCollection left, GeometryCollection right)
		{
			return left.geometries.SequenceEqual(right.geometries);
		}

		/// <summary>
		///     Determines whether the specified line strings are considered not equal.
		/// </summary>
		public static bool operator !=(GeometryCollection left, GeometryCollection right)
		{
			return !(left == right);
		}
	}
}
