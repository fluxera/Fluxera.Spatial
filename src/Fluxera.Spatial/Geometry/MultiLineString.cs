// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="MultiLineString" /> is an array of line strings.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.5
	/// </remarks>
	[PublicAPI]
	public struct MultiLineString : IGeometry
	{
		/// <summary>
		///     Represents an empty <see cref="MultiLineString" />.
		/// </summary>
		public static readonly MultiLineString Empty = new MultiLineString();

		/// <summary>
		///     Creates a new instance of the <see cref="MultiLineString" /> type.
		/// </summary>
		public MultiLineString()
		{
			this.Coordinates = Array.Empty<LineString>();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiLineString" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public MultiLineString(IEnumerable<LineString> coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<LineString>()).ToArray();
		}

		/// <summary>
		///     Creates a new instance of the <see cref="MultiLineString" /> type.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		public MultiLineString(params LineString[] coordinates)
		{
			this.Coordinates = (coordinates ?? Enumerable.Empty<LineString>()).ToArray();
		}

		/// <summary>
		///     Gets the coordinates.
		/// </summary>
		public LineString[] Coordinates { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if(obj is not MultiLineString multiLineString)
			{
				return false;
			}

			return this == multiLineString;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();

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
		///     Determines whether the specified multi line strings are considered equal.
		/// </summary>
		public static bool operator ==(MultiLineString left, MultiLineString right)
		{
			return left.Coordinates.SequenceEqual(right.Coordinates);
		}

		/// <summary>
		///     Determines whether the specified multi line strings are considered not equal.
		/// </summary>
		public static bool operator !=(MultiLineString left, MultiLineString right)
		{
			return !(left == right);
		}

		/// <inheritdoc />
		public bool HasAltitude => this.Coordinates.Any(x => x.HasAltitude);
	}
}
