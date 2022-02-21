// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System.Collections.Generic;
	using System.Globalization;
	using JetBrains.Annotations;

	/// <summary>
	///     The position is the fundamental geometry construct, consisting of <see cref="Latitude" />,
	///     <see cref="Longitude" /> and (optionally) <see cref="Altitude" />.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.1.1
	/// </remarks>
	[PublicAPI]
	public struct Position
	{
		public static readonly Position Empty = new Position();

		private static readonly IEqualityComparer<double> doubleComparer = new DoubleComparer();

		/// <summary>
		///     Creates a new instance of the <see cref="Position" /> type.
		/// </summary>
		public Position() : this(0, 0)
		{
		}

		/// <summary>
		///     Creates a new instance of the <see cref="Position" /> type.
		/// </summary>
		/// <param name="longitude">The longitude (x coordinate).</param>
		/// <param name="latitude">The latitude (y coordinate).</param>
		/// <param name="altitude">The altitude (z coordinate) in meters.</param>
		public Position(double longitude, double latitude, double? altitude = null)
		{
			this.Longitude = longitude;
			this.Latitude = latitude;
			this.Altitude = altitude;
		}

		/// <summary>
		///     Gets the longitude (x coordinate).
		/// </summary>
		public double Longitude { get; }

		/// <summary>
		///     Gets the latitude (y coordinate).
		/// </summary>
		public double Latitude { get; }

		/// <summary>
		///     Gets the altitude in meters.
		/// </summary>
		public double? Altitude { get; }

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return this == (Position)obj;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			int hashCode = 37 ^ this.Longitude.GetHashCode();
			hashCode = (hashCode * 37) ^ this.Latitude.GetHashCode();
			hashCode = (hashCode * 37) ^ this.Altitude.GetValueOrDefault().GetHashCode();
			return hashCode;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Altitude.HasValue
				? string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}, Altitude: {2}", this.Latitude, this.Longitude, this.Altitude)
				: string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}", this.Latitude, this.Longitude);
		}

		/// <summary>
		///     Determines whether the specified positions are considered equal.
		/// </summary>
		public static bool operator ==(Position left, Position right)
		{
			return
				doubleComparer.Equals(left.Longitude, right.Longitude) &&
				doubleComparer.Equals(left.Latitude, right.Latitude) &&
				doubleComparer.Equals(left.Altitude.GetValueOrDefault(), right.Altitude.GetValueOrDefault());
		}

		/// <summary>
		///     Determines whether the specified positions are considered not equal.
		/// </summary>
		public static bool operator !=(Position left, Position right)
		{
			return !(left == right);
		}
	}
}
